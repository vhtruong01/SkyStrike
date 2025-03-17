namespace SkyStrike
{
    namespace Enemy
    {
        public interface IEnemy
        {
            public IEnemyData data { get; set; }
            public void TakeDamage(int damage);
            public void Die();
        }

    }
}