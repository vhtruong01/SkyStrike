using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class PointMenu : SubMenu<PointDataObserver>, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private Vector2Property position;
        [SerializeField] private FloatProperty scale;
        [SerializeField] private FloatProperty rotation;
        [SerializeField] private FloatProperty standingTime;
        [SerializeField] private FloatProperty travelTime;
        [SerializeField] private BoolProperty shield;
        [SerializeField] private BoolProperty isStraightLine;
        [SerializeField] private BoolProperty isLookingAtPlayer;
        [SerializeField] private BoolProperty isImmortal;
        [SerializeField] private BoolProperty isIgnoreVelocity;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private BulletSelectionMenu bulletSelectionMenu;
        [SerializeField] private Button bulletSelectionMenuBtn;

        protected override void Preprocess()
        {
            base.Preprocess();
            bulletSelectionMenuBtn.onClick.AddListener(EnableBulletSelectionMenu);
            isLookingAtPlayer.BindToOtherProperty(rotation, false);
            isIgnoreVelocity.BindToOtherProperty(travelTime, true);
            isImmortal.BindToOtherProperty(shield, false);
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
            standingTime.Bind(data.standingTime);
            travelTime.Bind(data.travelTime);
            shield.Bind(data.shield);
            isImmortal.Bind(data.isImmortal);
            isStraightLine.Bind(data.isStraightLine);
            isIgnoreVelocity.Bind(data.isIgnoreVelocity);
            isLookingAtPlayer.Bind(data.isLookingAtPlayer);
            position.Bind(data.midPos, data.ChangePosition);
        }
        public override void UnbindData()
        {
            shield.Unbind();
            scale.Unbind();
            rotation.Unbind();
            travelTime.Unbind();
            standingTime.Unbind();
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