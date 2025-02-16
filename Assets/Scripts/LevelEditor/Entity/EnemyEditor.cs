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
            public EnemyDataObserver enemyDataObserver { get; private set; }

            public void SetData(EnemyDataObserver data)
            {
                enemyDataObserver = data;
                enemyDataObserver.position.Bind(SetPosition);
                enemyDataObserver.scale.Bind(SetScale);
                enemyDataObserver.rotation.Bind(SetRotation);
            }
            private void SetPosition(Vector2 pos)
            {
                transform.position = new(pos.x, pos.y, transform.position.z);
            }
            private void SetScale(Vector2 scale)
            {
                transform.localScale = new(scale.x, scale.y, transform.localScale.z);
            }
            private void SetRotation(float rotationZ)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,rotationZ);
            }
            public void OnDrag(PointerEventData eventData)
            {
                Vector3 newPos = mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y, 0, Screen.height),
                    0));
                newPos.z = transform.position.z;
                enemyDataObserver.position.SetData(newPos);
            }
            public void OnPointerDown(PointerEventData eventData)
            {
                MenuManager.SelectEnemy(enemyDataObserver);
            }
        }
    }
}