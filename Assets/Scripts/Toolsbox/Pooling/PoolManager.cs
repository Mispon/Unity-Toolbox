using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Toolsbox.Patterns;

namespace Assets.Scripts.Toolsbox.Pooling {
    /// <summary>
    /// Objects manager
    /// </summary>
    public class PoolManager : Singleton<PoolManager> {

        public int InitialCount = 3;
        public PoolBehavior[] Objects;

        /// <summary>
        /// Pool of game objects
        /// </summary>
        private readonly Dictionary<Type, List<PoolBehavior>> _objects = new Dictionary<Type, List<PoolBehavior>>();

        protected override void Awake() {
            base.Awake();
            Initialize();
        }

        /// <summary>
        /// Pre-fill pool
        /// </summary>
        private void Initialize() {
            foreach (var prefab in Objects) {
                var key = prefab.GetType();
                if (!_objects.ContainsKey(key)) {
                    _objects.Add(key, new List<PoolBehavior>());
                }
                for (var i = 0; i < InitialCount; i++) {
                    var obj = Instantiate(prefab, transform);
                    obj.Disable();
                    _objects[key].Add(obj);
                }
            }
        }

        /// <summary>
        /// Returns a list of objects from the pool
        /// </summary>
        public List<T> GetObjects<T>() where T : PoolBehavior {
            var key = typeof(T);
            ThrowIfKeyNotExist(key);
            var objects = _objects[key].Where(e => !e.IsActive()).OfType<T>().ToList();
            if (!objects.Any()) objects.Add(CreateObject<T>());
            return objects;
        }

        /// <summary>
        /// Returns an available game object
        /// </summary>
        public T GetObject<T>() where T : PoolBehavior {
            var key = typeof(T);
            ThrowIfKeyNotExist(key);
            return FindObject<T>() ?? CreateObject<T>();
        }

        /// <summary>
        /// Returns an existing object from the pool
        /// </summary>
        private T FindObject<T>() where T : PoolBehavior {
            var objects = _objects[typeof(T)];
            foreach (var obj in objects) {
                if (obj.IsActive()) continue;
                obj.Enable();
                return (T) obj;
            }
            return null;
        }

        /// <summary>
        /// Creates a new object in the pool
        /// </summary>
        private T CreateObject<T>() where T : PoolBehavior {
            var key = typeof(T);
            var prefab = Objects.First(e => e.GetType() == key);
            var result = Instantiate(prefab, transform) as T;
            _objects[key].Add(result);
            return result;
        }

        /// <summary>
        /// Throws an exception if the passed key is not in the pool
        /// </summary>
        private void ThrowIfKeyNotExist(Type key) {
            if (!_objects.ContainsKey(key)) {
                throw new ArgumentException($"Key {key.Name} not found in object pool");
            }
        }
    }
}
