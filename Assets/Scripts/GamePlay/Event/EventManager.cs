using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public enum EEventType
    {
        PlayNextWave,
        CreateEnemy,
        CreateItem,
        SlowTime,
        ShakeScreen,

    }
    public enum EEventSysType
    {
        PrepareGame,
        StartGame,
        EndGame,

    }
    public static partial class EventManager
    {
        private static Dictionary<EEventType, Delegate> events = new();
        private static Action<EEventSysType> sysEvent;
        public static void SubscribeSysEvent(Action<EEventSysType> sysAction)
            => sysEvent = sysAction;
        public static void Subscribe(EEventType type, UnityAction action)
        {
            if (action == null) return;
            if (events.ContainsKey(type))
                events[type] = Delegate.Combine(events[type], action);
            else events[type] = (action as Delegate);
        }
        public static void Unsubscribe(EEventType type, UnityAction action)
        {
            if (events.ContainsKey(type))
                events[type] = Delegate.Remove(events[type], action);
        }
        public static void Active(EEventType type)
            => events.GetValueOrDefault(type)?.DynamicInvoke();
        public static void Active(EEventSysType type)
            => sysEvent.Invoke(type);
    }
}