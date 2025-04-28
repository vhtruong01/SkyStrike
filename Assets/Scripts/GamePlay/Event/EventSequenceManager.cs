//using System.Collections.Generic;
//using System;
//using System.Collections;

//namespace SkyStrike.Game
//{
//    public static partial class EventManager
//    {
//        private static Dictionary<EEventChain, EventSequence> eventsSequence = new();
//        public static void Subscribe(EEventChain type, Func<IEnumerator> action, int order = -1)
//        {
//            if (action == null) return;
//            if (eventsSequence.TryGetValue(type, out var sequence))
//                sequence.AddChain(action, order);
//            else eventsSequence[type] = new(action, order);
//        }
//        public static void Unsubscribe(EEventChain type, Func<IEnumerator> action)
//        {
//            if (eventsSequence.TryGetValue(type, out var sequence))
//                sequence.RemoveChain(action);
//        }
//        public static void Active(EEventChain evt)
//        {
//            //eventsSequence.Getaction()
//        }
//    }
//}