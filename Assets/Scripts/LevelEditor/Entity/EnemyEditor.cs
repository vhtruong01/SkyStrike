using SkyStrike.Enemy;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyEditor : MonoBehaviour, IPointerDownHandler, IDragHandler
        {
            private static Camera _mainCam;
            private static Camera mainCam
            {
                get
                {
                    if (_mainCam == null)
                        _mainCam = Camera.main;
                    return _mainCam;
                }
            }
            public EnemyDataObserver enemyDataObserver { get; set; }

            public void OnDrag(PointerEventData eventData)
            {
                Vector3 newPos = mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y, 0, Screen.height),
                    0));
                newPos.z = transform.position.z;
                transform.position = newPos;
                enemyDataObserver.position.data = newPos;
            }
            public void OnPointerDown(PointerEventData eventData)
            {
                MenuManager.SelectEnemy(enemyDataObserver);
            }
        }
    }
}