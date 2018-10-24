using UnityEngine;

namespace Assets.Scripts.Patterns.UpdateControl {
    /// <summary>
    /// Определяет контролируемое поведение обновлений
    /// </summary>
    public class UpdateBehavior : MonoBehaviour {
        /// <summary>
        /// Обработчик обычного обновления
        /// </summary>
        public virtual void Tick() {}

        /// <summary>
        /// Обработчик фиксированного обновления
        /// </summary>
        public virtual void FixedTick() {}

        /// <summary>
        /// Обработчик позднего обновления
        /// </summary>
        public virtual void LateTick() {}
    }
}
