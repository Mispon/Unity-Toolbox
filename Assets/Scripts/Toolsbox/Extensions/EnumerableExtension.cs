using System.Collections.Generic;

namespace Assets.Scripts.Toolsbox.Extensions {
    /// <summary>
    /// Extensions for enumerables
    /// </summary>
    public static class EnumerableExtension {
        /// <summary>
        /// Create hash set from enumerable
        /// </summary>
        public static IEnumerable<T> ToHashSet<T>(this IEnumerable<T> source) {
            return new HashSet<T>(source);
        }
    }
}