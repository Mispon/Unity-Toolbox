using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Toolsbox.Patterns;
using UnityEngine;
using EventType = Assets.Scripts.Toolsbox.Patterns.EventType;

namespace Assets.Scripts.Toolsbox.Localization {
    /// <summary>
    /// Game localization logic
    /// NOTE: All localization files must be in "Assests/StreamingAssets" folder in .json format
    /// </summary>
    public class LocalizationManager : Singleton<LocalizationManager> {
        /// <summary>
        /// All localized text
        /// </summary>
        private Dictionary<string, string> _localizedTexts;

        public bool IsReady { get; private set; }
        public int LangValue { get; private set; }

        /// <summary>
        /// Returns a localized string
        /// </summary>
        public string Get(string key) {
            var result = key;
            if (_localizedTexts.ContainsKey(key)) {
                result = _localizedTexts[key];
            }
            return result;
        }

        /// <summary>
        /// Returns a localization key by value
        /// </summary>
        public string GetKey(string value) {
            return _localizedTexts.FirstOrDefault(e => e.Value == value).Key ?? "Not found";
        }

        /// <summary>
        /// Loading text
        /// </summary>
        public void LoadLocalization(int value, bool sendEvent = false) {
            IsReady = false;
            _localizedTexts = new Dictionary<string, string>();
#if UNITY_ANDROID
            StartCoroutine(ReadDataRoutine(value, raw => {
                ParseLocalizationData(raw, value, sendEvent);
            }));
#elif UNITY_IPHONE
            // todo: logic for iphone
#else
            var filePath = Path.Combine(Application.streamingAssetsPath, GetFileName(value));
            var jsonData = File.ReadAllText(filePath);
            ParseLocalizationData(jsonData, value, sendEvent);
#endif
        }

        /// <summary>
        /// Routine read localization data for android
        /// </summary>
        private IEnumerator ReadDataRoutine(int value, Action<string> callback) {
            var filePath = "jar:file://" + Application.dataPath + "!/assets/" + GetFileName(value);
            var www = new WWW(filePath);
            yield return www;
            callback(www.text);
        }

        /// <summary>
        /// Parsing localization data
        /// </summary>
        private void ParseLocalizationData(string jsonData, int value, bool sendEvent) {
            if (string.IsNullOrEmpty(jsonData)) {
                Debug.LogError("No localization file found");
                return;
            }
            var data = JsonUtility.FromJson<LocalizationData>(jsonData);
            foreach (var item in data.Items) {
                _localizedTexts.Add(item.Key, item.Value);
            }
            if (sendEvent) EventManager.RaiseEvent(EventType.LangChanged);
            LangValue = value;
            IsReady = true;
        }

        /// <summary>
        /// Returns the name of the localization file
        /// </summary>
        private static string GetFileName(int value) {
            switch (value) {
                case 1:
                    return "en.json";
                // todo: other langs
                default:
                    return "ru.json";
            }
        }
    }
}