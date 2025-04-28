namespace SkyStrike.Game
{
    public interface IDamageable : IObject
    {
        public bool TakeDamage(int damage);
    }
}