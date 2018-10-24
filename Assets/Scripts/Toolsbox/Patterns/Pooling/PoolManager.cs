using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Toolsbox.Patterns.Pooling {
    /// <summary>
    /// Мэнеджер объектов
    /// </summary>
    public class PoolManager : Singleton<PoolManager> {

        public int InitialCount = 3;
        public PoolBehavior[] Objects;

        /// <summary>
        /// Игровые объекты пула
        /// </summary>
        private readonly Dictionary<Type, List<PoolBehavior>> _objects = new Dictionary<Type, List<PoolBehavior>>();

        protected override void Awake() {
            base.Awake();
            Initialize();
        }

        /// <summary>
        /// Предварительное заполнение пула
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
        /// Возвращает список объектов из пула
        /// </summary>
        public List<T> GetObjects<T>() where T : PoolBehavior {
            var key = typeof(T);
            ThrowIfKeyNotExist(key);
            var objects = _objects[key].Where(e => !e.IsActive()).OfType<T>().ToList();
            if (!objects.Any()) objects.Add(CreateObject<T>());
            return objects;
        }

        /// <summary>
        /// Возвращает доступный игровой объект
        /// </summary>
        public T GetObject<T>() where T : PoolBehavior {
            var key = typeof(T);
            ThrowIfKeyNotExist(key);
            return FindObject<T>() ?? CreateObject<T>();
        }

        /// <summary>
        /// Возвращает существующий объект из пула
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
        /// Создает новый объект в пуле
        /// </summary>
        private T CreateObject<T>() where T : PoolBehavior {
            var key = typeof(T);
            var prefab = Objects.First(e => e.GetType() == key);
            var result = Instantiate(prefab, transform) as T;
            _objects[key].Add(result);
            return result;
        }

        /// <summary>
        /// Выбрасывает исключение, если переданный ключ отсутствует в пуле
        /// </summary>
        private void ThrowIfKeyNotExist(Type key) {
            if (!_objects.ContainsKey(key)) {
                throw new ArgumentException($"Ключ {key.Name} не найден в пуле объектов");
            }
        }
    }
}
