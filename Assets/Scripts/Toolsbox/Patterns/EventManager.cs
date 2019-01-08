using System;
using System.Collections.Generic;

namespace Assets.Scripts.Toolsbox.Patterns {
    /// <summary>
    /// Events types
    /// </summary>
    public enum EventType {
        StartGame,
        RestartGame,
        LangChanged
    }

    /// <summary>
    /// Event manager pattern
    /// </summary>
    public class EventManager {
        private static readonly Dictionary<EventType, List<Action<object[]>>> _handlers = 
            new Dictionary<EventType, List<Action<object[]>>>();

        /// <summary>
        /// Add new event handler
        /// </summary>
        public static void AddHandler(EventType type, Action<object[]> handler) {
            if (_handlers.ContainsKey(type)) {
                _handlers[type].Add(handler);
            }
            else {
                var handlers = new List<Action<object[]>> {handler};
                _handlers.Add(type, handlers);
            }
        }

        /// <summary>
        /// Remove event handler
        /// </summary>
        public static void RemoveHandler(EventType type, Action<object[]> handler) {
            if (_handlers.ContainsKey(type)) {
                _handlers[type].Remove(handler);
            }
        }

        /// <summary>
        /// Raise some event to all listeners
        /// </summary>
        public static void RaiseEvent(EventType type, params object[] args) {
            if (_handlers.ContainsKey(type)) {
                foreach (var handler in _handlers[type]) {
                    handler(args);
                }
            }
        }
    }
}