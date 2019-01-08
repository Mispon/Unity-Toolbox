using System.Collections;
using Assets.Scripts.Toolsbox.Patterns;
using UnityEngine;
using UnityEngine.UI;
using EventType = Assets.Scripts.Toolsbox.Patterns.EventType;

namespace Assets.Scripts.Toolsbox.Localization {
    /// <summary>
    /// Localized text behavior
    /// </summary>
    public class LocalizedText : MonoBehaviour {
        private Text _textComponent;
        [SerializeField] private string _key;

        private void Awake() {
            _textComponent = GetComponent<Text>();
            EventManager.AddHandler(EventType.LangChanged, OnLangChanged);
        }

        private IEnumerator Start() {
            while (!LocalizationManager.Instance.IsReady) yield return null;
            _textComponent.text = LocalizationManager.Instance.Get(_key);
        }

        /// <summary>
        /// Language change handler
        /// </summary>
        private void OnLangChanged(object[] args) {
            _textComponent.text = LocalizationManager.Instance.Get(_key);
        }

        private void OnDestroy() {
            EventManager.RemoveHandler(EventType.LangChanged, OnLangChanged);
        }
    }
}