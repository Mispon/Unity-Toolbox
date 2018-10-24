using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Toolsbox.Patterns.Pooling {
    /// <summary>
    /// Базовый класс всех объектов, используемых в пуле
    /// </summary>
    public class PoolBehavior : MonoBehaviour {
        /// <summary>
        /// Активирует объект
        /// </summary>
        public void Enable() {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Деактивирует объект
        /// </summary>
        public void Disable() {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Деактивирует объект с задержкой
        /// </summary>
        public void DisableAfter(float delay) {
            StartCoroutine(DisableRoutine(delay));
        }

        /// <summary>
        /// Проверяет, является ли объект активным
        /// </summary>
        public bool IsActive() {
            return gameObject.activeInHierarchy;
        }

        /// <summary>
        /// Корутина деактивации объекта
        /// </summary>
        private IEnumerator DisableRoutine(float delay) {
            yield return new WaitForSeconds(delay);
            Disable();
        }
    }
}
