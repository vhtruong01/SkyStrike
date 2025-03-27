//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace SkyStrike
//{
//    namespace Editor
//    {
//        public class Point : MonoBehaviour
//        {
//            [SerializeField] private Image prevPointIcon;
//            [SerializeField] private Image nextPointIcon;
//            [SerializeField] private Image connectionLine;
//            private Point prevPoint;
//            private Point nextPoint;

//            public UnityEvent<Vector2> onDrag {  get; private set; }

//            public void Awake()
//            {
//                onDrag = new();
//            }

//            public void OnDrag(Vector2 pos)
//            {

//            }
//        }
//    }
//}