namespace SkyStrike.Game
{
    public interface IDamager : IObject
    {
        public int GetDamage();
        public void OnHit(IDamageable obj)
        {
            if (activeSelf && obj.TakeDamage(GetDamage()))
                AfterHit();
        }
        public void AfterHit();
    }
}