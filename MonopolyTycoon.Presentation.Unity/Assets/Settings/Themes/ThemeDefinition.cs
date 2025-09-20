using System.Collections.Generic;
using UnityEngine;

namespace MonopolyTycoon.Settings.Themes
{
    /// <summary>
    /// A ScriptableObject that defines the asset keys for a specific visual and audio theme.
    /// Designers can create instances of this asset in the Unity Editor to define new themes
    /// without changing code, fulfilling REQ-1-093.
    /// </summary>
    [CreateAssetMenu(fileName = "NewThemeDefinition", menuName = "Monopoly Tycoon/Theme Definition")]
    public class ThemeDefinition : ScriptableObject
    {
        [Header("Theme Metadata")]
        public string ThemeName = "New Theme";

        [Header("Board Assets")]
        [Tooltip("Addressable key for the main game board prefab.")]
        public string GameBoardPrefabKey;
        [Tooltip("List of Addressable keys for the available player token prefabs.")]
        public List<string> PlayerTokenPrefabKeys;
        [Tooltip("Addressable key for the House model prefab.")]
        public string HouseModelPrefabKey;
        [Tooltip("Addressable key for the Hotel model prefab.")]
        public string HotelModelPrefabKey;

        [Header("UI Assets")]
        [Tooltip("Addressable key for the main HUD canvas prefab.")]
        public string HUDPrefabKey;
        [Tooltip("Addressable key for a UI Sprite Atlas containing common icons for this theme.")]
        public string UISpriteAtlasKey;

        [Header("Audio Assets")]
        [Tooltip("Addressable key for the main menu background music AudioClip.")]
        public string MainMenuMusicKey;
        [Tooltip("Addressable key for the in-game background music AudioClip.")]
        public string InGameMusicKey;
        [Tooltip("Addressable key for the dice roll sound effect AudioClip.")]
        public string DiceRollSfxKey;
        [Tooltip("Addressable key for a positive transaction sound (e.g., collecting rent).")]
        public string PositiveSfxKey;
        [Tooltip("Addressable key for a negative transaction sound (e.g., paying tax).")]
        public string NegativeSfxKey;
    }
}