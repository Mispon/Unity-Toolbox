using System.Collections.Generic;
using Assets.Scripts.Toolsbox.Patterns;

namespace Assets.Scripts.Toolsbox.UpdateBehavior {
    /// <summary>
    /// Manage game updates
    /// </summary>
    public class UpdateManager : Singleton<UpdateManager> {
        /// <summary>
        /// Updates handlers
        /// </summary>
        private readonly List<ITick> _updateHandlers = new List<ITick>();
        private readonly List<IFixedTick> _fixedUpdateHandlers = new List<IFixedTick>();
        private readonly List<ILateTick> _lateUpdateHandlers = new List<ILateTick>();

        /// <summary>
        /// Single regular update
        /// </summary>
        private void Update () {
            foreach (var handler in _updateHandlers) {
                handler.Tick();
            }
        }

        /// <summary>
        /// Single fixed update
        /// </summary>
        private void FixedUpdate() {
            foreach (var handler in _fixedUpdateHandlers) {
                handler.FixedTick();
            }
        }

        /// <summary>
        /// Single late update
        /// </summary>
        private void LateUpdate() {
            foreach (var handler in _lateUpdateHandlers) {
                handler.LateTick();
            }
        }

        /// <summary>
        /// Add updates handler
        /// </summary>
        public void AddHandler<T>(T handler) {
            if (handler is ITick tick) _updateHandlers.Add(tick);
            if (handler is IFixedTick fixedTick) _fixedUpdateHandlers.Add(fixedTick);
            if (handler is ILateTick lateTick) _lateUpdateHandlers.Add(lateTick);
        }

        /// <summary>
        /// Remove updates handler
        /// </summary>
        public void RemoveHandler<T>(T handler) {
            if (handler is ITick tick) _updateHandlers.Remove(tick);
            if (handler is IFixedTick fixedTick) _fixedUpdateHandlers.Remove(fixedTick);
            if (handler is ILateTick lateTick) _lateUpdateHandlers.Remove(lateTick);
        }
    }
}
