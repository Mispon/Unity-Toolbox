using System.Collections.Generic;

namespace Assets.Scripts.Toolbox.Extensions {
    /// <summary>
    /// Класс-расширение для перечислений
    /// </summary>
    public static class EnumerableExtension {
        /// <summary>
        /// Создает хэшсет из перечисления
        /// </summary>
        public static IEnumerable<T> ToHashSet<T>(this IEnumerable<T> source) {
            return new HashSet<T>(source);
        }
    }
}