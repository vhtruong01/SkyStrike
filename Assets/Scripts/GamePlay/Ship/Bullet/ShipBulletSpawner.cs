using System;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipBulletSpawner : MonoBehaviour
    {
        private float elaspedTime;
        private Func<ShipBulletData, Vector3, ShipBullet> onCreateBullet;
        private UnityAction onSpawnBullet;
        private ShipBulletData bulletData;
        public bool isEnabled { get; set; }

        public void Update()
        {
            if (!isEnabled) return;
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= bulletData.timeCooldown)
            {
                onSpawnBullet.Invoke();
                elaspedTime = 0;
            }
        }
        public void Upgrade() => bulletData.LevelUp();
        public void Init(ShipBulletData bulletData, Func<ShipBulletData, Vector3, ShipBullet> onCreateBullet)
        {
            elaspedTime = 0;
            isEnabled = true;
            this.bulletData = bulletData;
            this.onCreateBullet = onCreateBullet;
            switch (bulletData.metaData.type)
            {
                case EShipBulletType.NormalBullet:
                    onSpawnBullet = SpawnNormalBullet;
                    break;
                case EShipBulletType.DoubleBullet:
                    onSpawnBullet = SpawnDoubleBullet;
                    break;
                case EShipBulletType.TripleBullet:
                    onSpawnBullet = SpawnTripleBullet;
                    break;
                case EShipBulletType.MagicBullet:
                    onSpawnBullet = SpawnMagicBullet;
                    break;
                case EShipBulletType.RocketBullet:
                    onSpawnBullet = SpawnRocketBullet;
                    break;
            }
        }
        private void SpawnNormalBullet()
            => SpawnBullet(transform.position, new(0, bulletData.speed, 0));
        private void SpawnTripleBullet()
        {
            SpawnBullet(transform.position, new(-bulletData.speed * Mathf.Sin(Mathf.PI / 12), bulletData.speed, 0));
            SpawnBullet(transform.position, new(0, bulletData.speed, 0));
            SpawnBullet(transform.position, new(bulletData.speed * Mathf.Sin(Mathf.PI / 12), bulletData.speed, 0));
        }
        private void SpawnDoubleBullet()
        {
            SpawnBullet(transform.position + new Vector3(-0.25f, 0, 0), new(0, bulletData.speed, 0));
            SpawnBullet(transform.position + new Vector3(0.25f, 0, 0), new(0, bulletData.speed, 0));
        }
        private void SpawnMagicBullet()
        {

        }
        private void SpawnRocketBullet()
        {

        }
        private void SpawnBullet(Vector3 pos, Vector3 velocity)
            => onCreateBullet.Invoke(bulletData, pos).SetVelocity(velocity);
    }
}