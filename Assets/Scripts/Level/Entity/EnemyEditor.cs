using SkyStrike.Enemy;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyEditor : MonoBehaviour, IPointerClickHandler, IDragHandler
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
            private IEnemyData _data;
            public IEnemyData data
            {
                get => data;
                set
                {
                    _data = value;
                    //transform.position=value.position;

                }
            }

            public void OnDrag(PointerEventData eventData)
            {
                Vector3 newPos = mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y, 0, Screen.height),
                    0));
                newPos.z = transform.position.z;
                transform.position = newPos;
            }
            public void OnPointerClick(PointerEventData eventData)
            {
                MenuManager.SelectEnemy(data);
            }
        }
    }
}