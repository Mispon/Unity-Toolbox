using System;
using System.ComponentModel;

namespace Assets.Scripts.Toolsbox.Extensions {
    /// <summary>
    /// Extensions for enums
    /// </summary>
    public static class EnumExtension {
        /// <summary>
        /// Return enum's description
        /// </summary>
        public static string GetDescription(this Enum value) {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }

        /// <summary>
        /// Return enum's value from description
        /// </summary>
        public static T GetFromDescription<T>(string desc) {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields()) {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null) {
                    if (attribute.Description == desc) return (T) field.GetValue(null);
                }
                else {
                    if (field.Name == desc) return (T) field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found");
        }
    }
}