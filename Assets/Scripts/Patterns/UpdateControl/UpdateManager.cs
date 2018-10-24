using System;
using System.Collections.Generic;

namespace Assets.Scripts.Patterns.UpdateControl {
    [Flags]
    public enum UpdateType {
        Common,
        Fixed,
        Late
    }

    /// <summary>
    /// Управление апдейтами игры
    /// </summary>
    public class UpdateManager : Singleton<UpdateManager> {
        /// <summary>
        /// Обработчики апдейтов
        /// </summary>
        private readonly List<UpdateBehavior> _updateHandlers = new List<UpdateBehavior>();
        private readonly List<UpdateBehavior> _fixedUpdateHandlers = new List<UpdateBehavior>();
        private readonly List<UpdateBehavior> _lateUpdateHandlers = new List<UpdateBehavior>();

        /// <summary>
        /// Обычное обновление
        /// </summary>
        private void Update () {
            foreach (var handler in _updateHandlers) {
                handler.Tick();
            }
        }

        /// <summary>
        /// Обновление с фиксированной частотой
        /// </summary>
        private void FixedUpdate() {
            foreach (var handler in _fixedUpdateHandlers) {
                handler.FixedTick();
            }
        }

        /// <summary>
        /// Позднее обновление, вызывается после всех обычных и фиксированных обновлений
        /// </summary>
        private void LateUpdate() {
            foreach (var handler in _lateUpdateHandlers) {
                handler.LateTick();
            }
        }

        /// <summary>
        /// Добавляет новый обработчик обновлений
        /// </summary>
        public void AddHandler(UpdateBehavior handler, UpdateType type) {
            if (type.HasFlag(UpdateType.Common)) _updateHandlers.Add(handler);
            if (type.HasFlag(UpdateType.Fixed)) _fixedUpdateHandlers.Add(handler);
            if (type.HasFlag(UpdateType.Late)) _lateUpdateHandlers.Add(handler);
        }

        /// <summary>
        /// Удаляет обработчик обновлений
        /// </summary>
        public void RemoveHandler(UpdateBehavior handler) {
            if (_updateHandlers.Contains(handler)) _updateHandlers.Remove(handler);
            if (_fixedUpdateHandlers.Contains(handler)) _fixedUpdateHandlers.Remove(handler);
            if (_lateUpdateHandlers.Contains(handler)) _lateUpdateHandlers.Remove(handler);
        }
    }
}
