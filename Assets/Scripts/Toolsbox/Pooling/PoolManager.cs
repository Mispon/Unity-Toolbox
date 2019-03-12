using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Toolsbox.Patterns;
using UnityEngine;

namespace Assets.Scripts.Toolsbox.Pooling {
    /// <summary>
    /// Objects manager
    /// </summary>
    public class PoolManager : Singleton<PoolManager> {

        [SerializeField] private Pool[] _pools;

        /// <summary>
        /// Pool of game objects
        /// </summary>
        private readonly Dictionary<PoolType, List<PoolBehavior>> _pool = new Dictionary<PoolType, List<PoolBehavior>>();

        protected override void ChildAwake() {
            Initialize();
        }

        /// <summary>
        /// Pre-fill pool
        /// </summary>
        private void Initialize() {
            foreach (var pool in _pools) {
                _pool.Add(pool.Type, new List<PoolBehavior>(pool.Count));
                for (var i = 0; i < pool.Count; i++) {
                    AddOne(pool.Type, pool.Prefab);
                }
            }
        }

        /// <summary>
        /// Returns available object from pool
        /// </summary>
        public PoolBehavior Get(PoolType type) {
            ThrowIfKeyNotExist(type);
            var objects = _pool[type];
            if (objects.All(e => e.IsActive())) {
                AddOne(type, objects.First());
            }
            return objects.First(e => !e.IsActive());
        }

        /// <summary>
        /// Returns all objects from pool
        /// </summary>
        public List<PoolBehavior> GetAll(PoolType type) {
            ThrowIfKeyNotExist(type);
            return _pool[type];
        }

        /// <summary>
        /// Add new object to pool
        /// </summary>
        private void AddOne(PoolType type, PoolBehavior prefab) {
            var obj = Instantiate(prefab, transform);
            obj.Disable();
            _pool[type].Add(obj);
        }

        /// <summary>
        /// Throws an exception if the passed key is not in the pool
        /// </summary>
        private void ThrowIfKeyNotExist(PoolType type) {
            if (!_pool.ContainsKey(type)) {
                throw new ArgumentException($"Type {type} not exist in objects pool");
            }
        }
    }
}
