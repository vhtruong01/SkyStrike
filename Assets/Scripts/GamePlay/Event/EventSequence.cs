//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace SkyStrike.Game
//{
//    public class EventSequence
//    {
//        private EventChain eventChain;

//        public EventSequence() { }
//        public EventSequence(Func<IEnumerator> action, int depth)
//            => AddChain(action, depth);
//        public void AddChain(Func<IEnumerator> action, int depth = -1)
//        {
//            if (eventChain == null || (depth != -1 && eventChain.depth > depth))
//            {
//                EventChain temp = eventChain?.nextChain;
//                eventChain = EventChain.CreateEventChain(action, Mathf.Max(depth, 0));
//                eventChain.nextChain = temp;
//            }
//            else eventChain.Chain(action, depth);
//        }
//        public void RemoveChain(Func<IEnumerator> action)
//        {
//            if (eventChain.action == action)
//                eventChain = eventChain.nextChain;
//            else eventChain?.Unchain(action);
//        }
//        public List<IEnumerator> GetEnumerators()
//        {
//            List<IEnumerator> result = new();
//            EventChain temp = eventChain;
//            while (temp != null)
//            {
//                if (temp.action != null)
//                    result.Add(temp.action.Invoke());
//                temp = temp.nextChain;
//            }
//            return result;
//        }
//        public void Test()
//        {
//            string rs = "";
//            EventChain temp = eventChain;
//            int depth = 0;
//            while (temp != null && depth < 10)
//            {
//                depth++;
//                rs += temp.depth.ToString() + " ";
//                temp = temp.nextChain;
//            }
//            Debug.Log(rs);
//        }
//        private class EventChain
//        {
//            public Func<IEnumerator> action { get; set; }
//            public EventChain nextChain { get; set; }
//            public int depth { get; set; }
//            public void Chain(Func<IEnumerator> action, int depth)
//            {
//                if (nextChain == null)
//                {
//                    if (depth < 0) depth = Mathf.Max(depth, this.depth);
//                    nextChain = CreateEventChain(action, depth);
//                    return;
//                }
//                if (nextChain.depth <= depth || depth < 0)
//                    nextChain.Chain(action, depth);
//                else
//                {
//                    var temp = nextChain;
//                    nextChain = CreateEventChain(action, depth);
//                    nextChain.nextChain = temp;
//                }
//            }
//            public void Unchain(Func<IEnumerator> action)
//            {
//                if (nextChain.action == action)
//                    nextChain = nextChain.nextChain;
//                else nextChain.Unchain(action);
//            }
//            public static EventChain CreateEventChain(Func<IEnumerator> action, int depth)
//                => new()
//                {
//                    action = action,
//                    depth = depth,
//                    nextChain = null
//                };
//        }
//    }
//}