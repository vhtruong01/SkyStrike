using System.Collections.Generic;
using System;

namespace SkyStrike.Game
{
    public static partial class EventManager
    {
        private static Dictionary<Type, Delegate> eventsData = new();
        public static void Subscribe<T>(Action<T> action) where T : IEventData
        {
            if (action == null) return;
            Type type = typeof(T);
            if (eventsData.TryGetValue(type, out Delegate func))
                eventsData[type] = Delegate.Combine(func, action);
            else eventsData[type] = (action as Delegate);
        }
        public static void Unsubscribe<T>(Action<T> action) where T : IEventData
        {
            Type type = typeof(T);
            if (eventsData.TryGetValue(type, out Delegate func))
                eventsData[type] = Delegate.Remove(func, action);
        }
        public static void Active(IEventData data)
        {
            if (eventsData.TryGetValue(data.GetType(), out Delegate func))
                func.DynamicInvoke(data);
        }
    }
}