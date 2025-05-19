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
        StopTime,
        ShakeScreen,
        PrepareGame,
        StartGame,
        WinGame,
        LoseGame,
        Warning,
        CloseScene
    }
    public static partial class EventManager
    {
        private static Dictionary<EEventType, Delegate> events = new();
        public static void Subscribe(EEventType type, UnityAction action)
            => Subscribe(type, action as Delegate);
        public static void Unsubscribe(EEventType type, UnityAction action)
            => Unsubscribe(type, action as Delegate);
        private static void Subscribe(EEventType type, Delegate action)
        {
            if (action == null) return;
            if (events.ContainsKey(type))
                events[type] = Delegate.Combine(events[type], action);
            else events[type] = action;
        }
        private static void Unsubscribe(EEventType type, Delegate action)
        {
            if (events.ContainsKey(type))
                events[type] = Delegate.Remove(events[type], action);
        }
        public static void Active(EEventType type)
            => events.GetValueOrDefault(type)?.DynamicInvoke();
    }
}