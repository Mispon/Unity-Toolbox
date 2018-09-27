using UnityEngine;

namespace Assets.Scripts.Toolbox.Patterns {
    /// <summary>
    /// Паттерн "Одиночка"
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        /// <summary>
        /// Ссылка на единственный экземпляр класса
        /// </summary>
        public static T Instance { get; private set; }

        protected virtual void Awake() {
            if (Instance == null) {
                Instance = GetInstance();
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Возвращает единственный объект
        /// </summary>
        private static T GetInstance() {
            return FindObjectOfType<T>() ?? CreateInstance();
        }

        /// <summary>
        /// Создает новый объект на сцене
        /// </summary>
        private static T CreateInstance() {
            var singleton = new GameObject($"{nameof(T)} (Singleton)");
            DontDestroyOnLoad(singleton);
            return singleton.AddComponent<T>();
        }
    }
}