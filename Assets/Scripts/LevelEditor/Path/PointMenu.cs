using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class PointMenu : SubMenu<PointDataObserver>, IPointerClickHandler
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private FloatProperty scale;
            [SerializeField] private FloatProperty rotation;
            [SerializeField] private FloatProperty accleration;
            [SerializeField] private FloatProperty standingTime;
            [SerializeField] private FloatProperty travelTime;
            [SerializeField] private BoolProperty isFixedRotation;
            [SerializeField] private BoolProperty isTraightLine;
            [SerializeField] private BoolProperty isLookAtPlayer;
            [SerializeField] private BoolProperty isImmortal;

            public override void BindData()
            {
                scale.Bind(data.scale);
                rotation.Bind(data.rotation);
                accleration.Bind(data.accleration);
                standingTime.Bind(data.standingTime);
                isFixedRotation.Bind(data.isFixedRotation);
                isImmortal.Bind(data.isImmortal);
                isTraightLine.Bind(data.isTraightLine);
                isLookAtPlayer.Bind(data.isLookAtPlayer);
                position.Bind(data.midPos, data.Translate);
            }
            public void OnPointerClick(PointerEventData eventData) { }
            public override void UnbindData()
            {
                scale.Unbind();
                rotation.Unbind();
                accleration.Unbind();
                standingTime.Unbind();
                isFixedRotation.Unbind();
                isTraightLine.Unbind();
                isLookAtPlayer.Unbind();
                isImmortal.Unbind();
                position.Unbind();
            }
        }
    }
}