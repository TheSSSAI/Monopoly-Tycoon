using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MonopolyTycoon.Settings.Themes
{
    /// <summary>
    /// A ScriptableObject that defines the asset keys for a specific visual/audio theme.
    /// This allows designers to create new themes by creating new instances of this asset
    /// in the Unity Editor and populating its fields with Addressable asset references.
    /// Fulfills REQ-1-093.
    /// </summary>
    [CreateAssetMenu(fileName = "NewThemeDefinition", menuName = "Monopoly Tycoon/Theme Definition")]
    public class ThemeDefinition : ScriptableObject
    {
        [Header("Theme Metadata")]
        public string ThemeName = "New Theme";

        [Header("Board Assets")]
        public AssetReferenceGameObject GameBoardPrefab;
        public List<AssetReferenceGameObject> PlayerTokenPrefabs;
        
        [Header("UI Assets")]
        public AssetReferenceGameObject HudCanvasPrefab;
        public AssetReferenceGameObject MainMenuCanvasPrefab;
        // Could add references to UI Toolkit stylesheets, fonts, etc.

        [Header("Audio Assets")]
        public AssetReferenceT<AudioClip> MainMenuMusic;
        public AssetReferenceT<AudioClip> InGameMusic;
        
        [Header("Sound Effects")]
        public AssetReferenceT<AudioClip> DiceRollSfx;
        public AssetReferenceT<AudioClip> TokenMoveSfx;
        public AssetReferenceT<AudioClip> PurchaseSfx;
        public AssetReferenceT<AudioClip> RentPaidSfx;

        [Header("Victory/Loss Screens")]
        public AssetReferenceGameObject VictoryScreenPrefab;
        public AssetReferenceGameObject GameOverScreenPrefab;
    }
}