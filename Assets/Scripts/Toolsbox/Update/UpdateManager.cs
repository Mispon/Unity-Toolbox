using System;
using System.Collections.Generic;
using Assets.Scripts.Toolsbox.Patterns;

namespace Assets.Scripts.Toolsbox.Update {
    [Flags]
    public enum UpdateType {
        Common,
        Fixed,
        Late
    }

    /// <summary>
    /// Manage game updates
    /// </summary>
    public class UpdateManager : Singleton<UpdateManager> {
        /// <summary>
        /// Updates handlers
        /// </summary>
        private readonly List<IUpdateBehavior> _updateHandlers = new List<IUpdateBehavior>();
        private readonly List<IUpdateBehavior> _fixedUpdateHandlers = new List<IUpdateBehavior>();
        private readonly List<IUpdateBehavior> _lateUpdateHandlers = new List<IUpdateBehavior>();

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
        public void AddHandler(IUpdateBehavior handler, UpdateType type) {
            if (type.HasFlag(UpdateType.Common)) _updateHandlers.Add(handler);
            if (type.HasFlag(UpdateType.Fixed)) _fixedUpdateHandlers.Add(handler);
            if (type.HasFlag(UpdateType.Late)) _lateUpdateHandlers.Add(handler);
        }

        /// <summary>
        /// Remove updates handler
        /// </summary>
        public void RemoveHandler(IUpdateBehavior handler) {
            if (_updateHandlers.Contains(handler)) _updateHandlers.Remove(handler);
            if (_fixedUpdateHandlers.Contains(handler)) _fixedUpdateHandlers.Remove(handler);
            if (_lateUpdateHandlers.Contains(handler)) _lateUpdateHandlers.Remove(handler);
        }
    }
}
