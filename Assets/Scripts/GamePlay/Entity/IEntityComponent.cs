using UnityEngine.Events;

namespace SkyStrike.Game
{
    public interface IEntityComponent : IObject
    {
        public IEntity entity { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Interrupt();
        public void Init() { }
    }
}