using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class PointMenu : SubMenu<PointDataObserver>, IDragHandler, IPointerDownHandler
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private FloatProperty scale;
            [SerializeField] private FloatProperty rotation;
            [SerializeField] private FloatProperty accleration;
            [SerializeField] private FloatProperty standingTime;
            [SerializeField] private FloatProperty travelTime;
            [SerializeField] private BoolProperty isFixedRotation;
            [SerializeField] private BoolProperty isStraightLine;
            [SerializeField] private BoolProperty isLookAtPlayer;
            [SerializeField] private BoolProperty isImmortal;
            [SerializeField] private TextMeshProUGUI title;
            [SerializeField] private BulletSelectionMenu bulletSelectionMenu;
            [SerializeField] private Button bulletSelectionMenuBtn;

            public void Awake()
                => bulletSelectionMenuBtn.onClick.AddListener(EnableBulletSelectionMenu);
            private void EnableBulletSelectionMenu()
            {
                bool isEnable = bulletSelectionMenu.gameObject.activeSelf;
                bulletSelectionMenu.gameObject.SetActive(!isEnable);
            }
            public override void BindData()
            {
                scale.Bind(data.scale);
                rotation.Bind(data.rotation);
                accleration.Bind(data.accleration);
                standingTime.Bind(data.standingTime);
                travelTime.Bind(data.travelTime);
                isFixedRotation.Bind(data.isFixedRotation);
                isImmortal.Bind(data.isImmortal);
                isStraightLine.Bind(data.isStraightLine);
                isLookAtPlayer.Bind(data.isLookAtPlayer);
                position.Bind(data.midPos, data.ChangePosition);
            }
            public override void UnbindData()
            {
                scale.Unbind();
                rotation.Unbind();
                travelTime.Unbind();
                accleration.Unbind();
                standingTime.Unbind();
                isFixedRotation.Unbind();
                isStraightLine.Unbind();
                isLookAtPlayer.Unbind();
                isImmortal.Unbind();
                position.Unbind();
            }
            public void SetTitle(string name) => title.text = name;
            public void OnDrag(PointerEventData eventData) { }
            public void OnPointerDown(PointerEventData eventData) { }
        }
    }
}