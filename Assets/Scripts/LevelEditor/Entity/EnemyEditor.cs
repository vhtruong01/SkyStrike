using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyEditor : MonoBehaviour, IPointerDownHandler, IDragHandler,IUIElement
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
            private Image bg;
            public EnemyDataObserver enemyDataObserver { get; private set; }
            public UnityEvent onClick {  get; set; }

            public void Awake()
            {
                onClick = new();
                bg = GetComponent<Image>();
                onClick.AddListener(() => MenuManager.SelectEnemy(enemyDataObserver));
            }
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
            public void OnPointerDown(PointerEventData eventData) => onClick.Invoke();
            public Image GetBackground() => bg;
            public void OnPointerClick(PointerEventData eventData) { }
        }
    }
}