using UnityEngine;

namespace Assets.Scripts.Toolsbox.Patterns {
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        /// <summary>
        /// Link to single instance of class
        /// </summary>
        public static T Instance { get; private set; }

        [SerializeField] private bool _dontDestroy = true;

        protected virtual void Awake() {
            if (Instance == null) {
                Instance = GetInstance();
                ChildAwake();
            }
            else if (Instance != this)
                Destroy(gameObject);
        }

        /// <summary>
        /// Child initialization
        /// </summary>
        protected virtual void ChildAwake() {}

        /// <summary>
        /// Returns a single object
        /// </summary>
        private T GetInstance() {
            var instance = FindObjectOfType<T>() ?? CreateInstance();
            if (_dontDestroy) DontDestroyOnLoad(instance);
            return instance;
        }

        /// <summary>
        /// Creates a new object on stage
        /// </summary>
        private static T CreateInstance() {
            var singleton = new GameObject($"{nameof(T)} (Singleton)");
            return singleton.AddComponent<T>();
        }
    }
}