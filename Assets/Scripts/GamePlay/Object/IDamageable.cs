namespace SkyStrike.Game
{
    public interface IDamageable : IObject
    {
        public bool TakeDamage(IDamager damager);
    }
}