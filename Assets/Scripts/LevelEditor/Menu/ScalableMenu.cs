using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ScalableMenu : EventNotifyMenu, IDragHandler, IBeginDragHandler, IEndDragHandler
        {
            [SerializeField] protected GridScreen screen;
            [SerializeField] protected NormalButton snapBtn;
            private Vector3 curPos;
            private Vector3 startPos;
            protected bool isDragging;

            public virtual void Start()
               => snapBtn.AddListener((b) => screen.isSnapping = b, () => screen.isSnapping);
            public override void Init()
            {
                base.Init();
                screen.Init();
            }
            public override void Restore()
            {
                base.Restore();
                string screenClassName = GetType().Name + "." + screen.GetType().Name;
                screen.scale = PlayerPrefs.GetFloat(screenClassName + ".scale", 0.8f);
                screen.isLocked = PlayerPrefs.GetInt(screenClassName + ".isLocked", 0) != 0;
                screen.isSnapping = PlayerPrefs.GetInt(screenClassName + ".isSnapping", 0) != 0;
                screen.transform.position = new(PlayerPrefs.GetFloat(screenClassName + ".position.x", 0),
                                                PlayerPrefs.GetFloat(screenClassName + ".position.y", 0),
                                                screen.transform.position.z);
            }
            public virtual void OnBeginDrag(PointerEventData eventData)
            {
                isDragging = true;
                curPos = screen.transform.position;
                startPos = eventData.pointerCurrentRaycast.worldPosition;
            }
            public virtual void OnDrag(PointerEventData eventData)
            {
                if (!screen.isLocked && eventData.position.x > 0 && eventData.position.y > 0 && eventData.position.x < Screen.width && eventData.position.y < Screen.height)
                    screen.SetPosition(curPos + (eventData.pointerCurrentRaycast.worldPosition - startPos));
            }
            public void OnEndDrag(PointerEventData eventData) => isDragging = false;
            public override void SaveSetting()
            {
                base.SaveSetting();
                string screenClassName = GetType().Name + "." + screen.GetType().Name;
                PlayerPrefs.SetFloat(screenClassName + ".scale", screen.scale);
                PlayerPrefs.SetInt(screenClassName + ".isLocked", screen.isLocked ? 1 : 0);
                PlayerPrefs.SetInt(screenClassName + ".isSnapping", screen.isSnapping ? 1 : 0);
                PlayerPrefs.SetFloat(screenClassName + ".position.x", screen.transform.position.x);
                PlayerPrefs.SetFloat(screenClassName + ".position.y", screen.transform.position.y);
            }
        }
    }
}