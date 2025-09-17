using MonopolyTycoon.Domain.AI.Common;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.ValueObjects;
using Panda;

namespace MonopolyTycoon.Domain.AI.BehaviorNodes.Actions
{
    public class TurnManagementActions
    {
        private readonly AIContext _context;

        public TurnManagementActions(AIContext context)
        {
            _context = context;
        }

        [Task]
        public void EndPreRollPhase()
        {
            // This action signifies the end of the pre-roll management phase (building, trading, etc.)
            // and transitions the AI to the roll phase.
            _context.ResultAction = new PlayerAction(PlayerActionType.RollDice);
            ThisTask.Succeed();
        }

        [Task]
        public void EndTurn()
        {
            // This is a fallback or explicit action to end the turn after all post-roll actions are complete.
            _context.ResultAction = new PlayerAction(PlayerActionType.EndTurn);
            ThisTask.Succeed();
        }

        [Task]
        public void DecideJailAction()
        {
            var aiPlayer = _context.CurrentPlayerState;
            
            // Strategy: Use card if available. If cash is high, pay the fine to not miss turns.
            // If cash is low, risk rolling the dice. Harder AIs are more willing to pay.
            if (aiPlayer.GetOutOfJailCards > 0)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.UseGetOutOfJailCard);
                ThisTask.Succeed();
                return;
            }

            bool shouldPay = (aiPlayer.Cash > _context.Parameters.MinimumCashReserve * 3) &&
                             (_context.Random.NextDouble() < _context.Parameters.RiskAversion);

            if (shouldPay)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.PayJailFine);
                ThisTask.Succeed();
                return;
            }

            // Default to rolling the dice
            _context.ResultAction = new PlayerAction(PlayerActionType.RollForDoubles);
            ThisTask.Succeed();
        }
    }
}