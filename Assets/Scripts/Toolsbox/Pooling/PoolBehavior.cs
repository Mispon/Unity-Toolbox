using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Toolsbox.Pooling {
    /// <summary>
    /// Базовый класс всех объектов, используемых в пуле
    /// </summary>
    public class PoolBehavior : MonoBehaviour {
        /// <summary>
        /// Activate object
        /// </summary>
        public void Enable() {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivate object
        /// </summary>
        public void Disable() {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Deactivate object with delay
        /// </summary>
        public void DisableAfter(float delay) {
            StartCoroutine(DisableRoutine(delay));
        }

        /// <summary>
        /// Check object's activity
        /// </summary>
        public bool IsActive() {
            return gameObject.activeInHierarchy;
        }

        /// <summary>
        /// Coroutine of deactivation object
        /// </summary>
        private IEnumerator DisableRoutine(float delay) {
            yield return new WaitForSeconds(delay);
            Disable();
        }
    }
}
