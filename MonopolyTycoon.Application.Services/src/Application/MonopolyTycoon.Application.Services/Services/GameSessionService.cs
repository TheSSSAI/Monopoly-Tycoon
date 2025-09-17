using FluentValidation;
using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions.Repositories;
using MonopolyTycoon.Application.Abstractions.Services;
using MonopolyTycoon.Application.Services.DTOs;
using MonopolyTycoon.Application.Services.Exceptions;
using MonopolyTycoon.Domain.Abstractions.Factories;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Abstractions.Events;

namespace MonopolyTycoon.Application.Services.Services;

public class GameSessionService : IGameSessionService
{
    private readonly ISaveGameRepository _saveGameRepository;
    private readonly IPlayerProfileRepository _playerProfileRepository;
    private readonly IDomainFactory _domainFactory;
    private readonly IEventBus _eventBus;
    private readonly ILogger<GameSessionService> _logger;
    private readonly IValidator<GameSetupOptions> _gameSetupValidator;

    private GameState? _currentGameState;

    public GameSessionService(
        ISaveGameRepository saveGameRepository,
        IPlayerProfileRepository playerProfileRepository,
        IDomainFactory domainFactory,
        IEventBus eventBus,
        ILogger<GameSessionService> logger,
        IValidator<GameSetupOptions> gameSetupValidator)
    {
        _saveGameRepository = saveGameRepository;
        _playerProfileRepository = playerProfileRepository;
        _domainFactory = domainFactory;
        _eventBus = eventBus;
        _logger = logger;
        _gameSetupValidator = gameSetupValidator;
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState ?? throw new SessionManagementException("There is no active game session.");
    }

    public async Task StartNewGameAsync(GameSetupOptions options, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to start a new game with player name: {PlayerName}", options.HumanPlayerName);

        var validationResult = await _gameSetupValidator.ValidateAsync(options, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning("New game setup options failed validation: {ValidationErrors}", errors);
            throw new ValidationException(errors);
        }

        try
        {
            var playerProfile = await _playerProfileRepository.GetOrCreateProfileAsync(options.HumanPlayerName, cancellationToken);
            _logger.LogInformation("Player profile retrieved or created for {PlayerName} with ID {ProfileId}", playerProfile.DisplayName, playerProfile.Id);

            _currentGameState = _domainFactory.CreateNewGame(options, playerProfile);
            _logger.LogInformation("New game state created with {PlayerCount} players.", _currentGameState.Players.Count);

            await _eventBus.PublishAsync(new GameStateUpdatedEvent(_currentGameState), cancellationToken);
            _logger.LogInformation("New game started successfully. Published GameStateUpdatedEvent.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "A critical error occurred while starting a new game.");
            throw new SessionManagementException("Failed to start a new game. See inner exception for details.", ex);
        }
    }

    public async Task LoadGameAsync(int slot, CancellationToken cancellationToken = default)
    {
        if (slot < 1) // Assuming slots are 1-based
        {
            throw new ArgumentOutOfRangeException(nameof(slot), "Save slot must be a positive integer.");
        }

        _logger.LogInformation("Attempting to load game from slot {SlotNumber}", slot);
        
        try
        {
            var loadedState = await _saveGameRepository.LoadAsync(slot, cancellationToken);
            _currentGameState = loadedState;
            _logger.LogInformation("Successfully loaded game from slot {SlotNumber}. Current turn: {TurnNumber}", slot, _currentGameState.CurrentTurn);

            await _eventBus.PublishAsync(new GameStateUpdatedEvent(_currentGameState), cancellationToken);
            _logger.LogInformation("Game load successful. Published GameStateUpdatedEvent.");
        }
        catch(FileNotFoundException ex)
        {
            _logger.LogWarning(ex, "Attempted to load from an empty or non-existent save slot: {SlotNumber}", slot);
            throw new SessionManagementException($"Save file for slot {slot} not found.", ex);
        }
        catch (Exception ex) // Catches potential corruption or deserialization errors from the repository
        {
            _logger.LogError(ex, "A critical error occurred while loading game from slot {SlotNumber}", slot);
            throw new SessionManagementException($"Failed to load game from slot {slot}. The file may be corrupt or incompatible.", ex);
        }
    }

    public async Task SaveGameAsync(int slot, CancellationToken cancellationToken = default)
    {
        if (_currentGameState is null)
        {
            throw new SessionManagementException("Cannot save game because there is no active game session.");
        }

        if (slot < 1) // Assuming slots are 1-based
        {
            throw new ArgumentOutOfRangeException(nameof(slot), "Save slot must be a positive integer.");
        }

        _logger.LogInformation("Attempting to save current game to slot {SlotNumber}", slot);
        
        try
        {
            await _saveGameRepository.SaveAsync(_currentGameState, slot, cancellationToken);
            _logger.LogInformation("Successfully saved game to slot {SlotNumber}", slot);
            
            await _eventBus.PublishAsync(new GameSavedEvent(slot), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "A critical error occurred while saving game to slot {SlotNumber}", slot);
            throw new SessionManagementException($"Failed to save game to slot {slot}. Please check disk space and permissions.", ex);
        }
    }
}