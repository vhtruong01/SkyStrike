namespace SkyStrike.Game
{
    public interface IEntity : IEntityComponent
    {
        public void DropItemAndDisappear();
        public void Disappear();
        public void Die();
    }
}