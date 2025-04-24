using UnityEngine.Events;

namespace SkyStrike.Game
{
    public interface IEntityComponent: IObject
    {
        public UnityAction<EEntityAction> notifyAction { get; set; }
        public void Interrupt();
    }
}