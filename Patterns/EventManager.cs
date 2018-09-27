using System;
using System.Collections.Generic;

namespace Assets.Scripts.Toolbox.Patterns {
    /// <summary>
    /// Типы событий
    /// </summary>
    public enum EventType {
        StartGame,
        RestartGame
    }

    /// <summary>
    /// Паттерн "Мэнеджер событий"
    /// </summary>
    public class EventManager {
        private static readonly Dictionary<EventType, List<Action>> _handlers = new Dictionary<EventType, List<Action>>();

        /// <summary>
        /// Добавляет обработчик события
        /// </summary>
        public static void AddHandler(EventType type, Action handler) {
            if (_handlers.ContainsKey(type)) {
                _handlers[type].Add(handler);
            }
            else {
                var handlers = new List<Action> {handler};
                _handlers.Add(type, handlers);
            }
        }

        /// <summary>
        /// Удаляет обработчик события
        /// </summary>
        public static void RemoveHandler(EventType type) {
            if (_handlers.ContainsKey(type)) {
                _handlers.Remove(type);
            }
        }

        /// <summary>
        /// Посылает сообщение об эвенте обработчикам
        /// </summary>
        public static void RaiseEvent(EventType type) {
            if (_handlers.ContainsKey(type)) {
                foreach (var handler in _handlers[type]) {
                    handler();
                }
            }
        }
    }
}