using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ExtraPoint : MonoBehaviour, IDragHandler
        {
            [field: SerializeField]public Image image { get; private set; }
            [SerializeField] private RectTransform connectionLine;
            public UnityAction<Vector2> call;

            public void OnDrag(PointerEventData eventData) => call?.Invoke(transform.position);
            public void Enable(bool isEnable)
            {
                gameObject.SetActive(isEnable);
                connectionLine.gameObject.SetActive(isEnable);
            }
            public void Render(Vector2 newPos, Vector2 midPos)
            {
                transform.position = newPos.SetZ(transform.position.z);
                Vector2 dir = newPos - midPos;
                var angle = -Vector2.SignedAngle(dir, Vector2.right);
                float len = (Controller.mainCam.WorldToScreenPoint(dir) - new Vector3(Screen.width / 2, Screen.height / 2)).magnitude;
                connectionLine.sizeDelta = new(len, connectionLine.sizeDelta.y);
                connectionLine.SetPositionAndRotation(
                    new((newPos.x + midPos.x) / 2, (newPos.y + midPos.y) / 2, connectionLine.position.z),
                    Quaternion.Euler(connectionLine.eulerAngles.SetZ(angle)));
            }
        }
    }
}