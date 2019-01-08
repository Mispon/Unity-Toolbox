namespace Assets.Scripts.Toolsbox.Localization {
    /// <summary>
    /// Localization data
    /// </summary>
    [System.Serializable]
    public class LocalizationData {
        public LocalizationItem[] Items;
    }

    /// <summary>
    /// Single element of localized text
    /// </summary>
    [System.Serializable]
    public class LocalizationItem {
        public string Key;
        public string Value;
    }
}