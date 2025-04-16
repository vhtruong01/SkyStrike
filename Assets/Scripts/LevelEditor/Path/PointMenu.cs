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
            [SerializeField] private BoolProperty isLookingAtPlayer;
            [SerializeField] private BoolProperty isImmortal;
            [SerializeField] private BoolProperty isIgnoreVelocity;
            [SerializeField] private TextMeshProUGUI title;
            [SerializeField] private BulletSelectionMenu bulletSelectionMenu;
            [SerializeField] private Button bulletSelectionMenuBtn;

            public override void Awake()
            {
                base.Awake();
                bulletSelectionMenuBtn.onClick.AddListener(EnableBulletSelectionMenu);
                isFixedRotation.BindToOtherProperty(rotation, true);
                isFixedRotation.BindToOtherProperty(isLookingAtPlayer, false);
                isLookingAtPlayer.BindToOtherProperty(rotation, false);
                isStraightLine.BindToOtherProperty(accleration, true);
                isIgnoreVelocity.BindToOtherProperty(travelTime, true);
            }
            private void EnableBulletSelectionMenu()
            {
                bool isEnabled = bulletSelectionMenu.gameObject.activeSelf;
                bulletSelectionMenu.gameObject.SetActive(!isEnabled);
            }
            public override void BindData()
            {
                scale.Bind(data.scale);
                rotation.Bind(data.rotation);
                accleration.Bind(data.accleration);
                standingTime.Bind(data.standingTime);
                travelTime.Bind(data.travelTime);
                isImmortal.Bind(data.isImmortal);
                isStraightLine.Bind(data.isStraightLine);
                isFixedRotation.Bind(data.isFixedRotation);
                isIgnoreVelocity.Bind(data.isIgnoreVelocity);
                isLookingAtPlayer.Bind(data.isLookingAtPlayer);
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
                isLookingAtPlayer.Unbind();
                isIgnoreVelocity.Unbind();
                isImmortal.Unbind();
                position.Unbind();
            }
            public void SetTitle(string name) => title.text = name;
            public void OnDrag(PointerEventData eventData) { }
            public void OnPointerDown(PointerEventData eventData) { }
        }
    }
}