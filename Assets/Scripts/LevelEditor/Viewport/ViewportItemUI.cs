using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUI : UIElement, IPointerDownHandler, IDragHandler
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
            [SerializeField] private Image icon;

            public override void SetData(IEditorData data)
            {
                this.data = data;
                var objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver.position.Bind(SetPosition);
                objectDataObserver.scale.Bind(SetScale);
                objectDataObserver.rotation.Bind(SetRotation);
                icon.sprite = objectDataObserver.metaData.data.sprite;
                icon.color = objectDataObserver.metaData.data.color;
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
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
            }
            public void OnDrag(PointerEventData eventData)
            {
                Vector3 newPos = mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y, 0, Screen.height),
                    0));
                newPos.z = transform.position.z;
                (data as ObjectDataObserver).position.SetData(newPos);
            }
            public void OnPointerDown(PointerEventData eventData) => InvokeData();
            public override void OnPointerClick(PointerEventData eventData) { }
        }
    }
}