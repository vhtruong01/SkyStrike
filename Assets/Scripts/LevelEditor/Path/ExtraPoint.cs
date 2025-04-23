using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class ExtraPoint : MonoBehaviour, IDragHandler
    {
        [field: SerializeField] public Image image { get; private set; }
        [SerializeField] private RectTransform connectionLine;
        public UnityAction<Vector2> call { private get; set; }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
            call?.Invoke(transform.position);
        }
        public void Enable(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
            connectionLine.gameObject.SetActive(isEnabled);
        }
        public void Render(Vector2 newPos, Vector2 midPos, float scale)
        {
            transform.position = newPos.SetZ(transform.position.z);
            Vector2 dir = newPos - midPos;
            var angle = -Vector2.SignedAngle(dir, Vector2.right);
            float len = (Controller.mainCam.WorldToScreenPoint(dir) - new Vector3(Screen.width / 2, Screen.height / 2)).magnitude / scale;
            connectionLine.sizeDelta = new(len, connectionLine.sizeDelta.y);
            connectionLine.SetPositionAndRotation(
                new((newPos.x + midPos.x) / 2, (newPos.y + midPos.y) / 2, connectionLine.position.z),
                Quaternion.Euler(connectionLine.eulerAngles.SetZ(angle)));
        }
    }
}