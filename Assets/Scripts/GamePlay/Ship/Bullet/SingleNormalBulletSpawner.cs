namespace SkyStrike
{
    namespace Ship
    {
        class SingleNormalBulletSpawner : BulletSpawner
        {
            public override void Spawn()
            {
                Bullet bullet = bulletPool.Get();
                bullet.transform.position = transform.position;
            }
        }
    }
}