namespace SkyStrike.Game
{
    public enum EDamageType
    {
        Normal,
        Piercing,
        Slashing,

    }
    public interface IDamager : IObject
    {
        public EDamageType damageType { get; }
        public int GetDamage();
        public void OnHit(IDamageable obj)
        {
            if (isActive && obj.TakeDamage(this))
                AfterHit();
        }
        public void AfterHit();
    }
}