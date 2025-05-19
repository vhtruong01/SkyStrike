using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class LaserSkill : Skill<LaserData>, IShipComponent, IDamager
    {
        [SerializeField] private LineRenderer previewLine;
        [SerializeField] private LineRenderer laserLine;
        private Color warningColor;
        public EDamageType damageType => skillData.damageType;

        public void Awake()
        {
            previewLine.gameObject.SetActive(false);
            laserLine.gameObject.SetActive(false);
            warningColor = previewLine.startColor;
        }
        public int GetDamage() => skillData.damage;
        public override void Execute()
            => coroutine = StartCoroutine(Fire());
        private IEnumerator Fire()
        {
            anim.Restart();
            previewLine.gameObject.SetActive(true);
            laserLine.gameObject.SetActive(false);
            float elapsedTime = 0;
            float time = 0.75f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.unscaledDeltaTime;
                previewLine.startColor = previewLine.endColor = warningColor.ChangeAlpha((1 - elapsedTime / time) * warningColor.a);
                yield return null;
            }
            previewLine.gameObject.SetActive(false);
            laserLine.gameObject.SetActive(true);
            elapsedTime = 0;
            time = 0.075f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                laserLine.startWidth = laserLine.endWidth = skillData.size * elapsedTime / time;
                yield return null;
            }
            elapsedTime = 0;
            time = skillData.duration;
            while (elapsedTime < time)
            {
                PulseLaser();
                elapsedTime += Mathf.Max(skillData.damageInterval, Time.deltaTime);
                yield return new WaitForSeconds(skillData.damageInterval);
            }
            elapsedTime = 0;
            time = 0.5f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.unscaledDeltaTime;
                laserLine.startWidth = laserLine.endWidth = skillData.size * (1 - Mathf.Pow(elapsedTime / time, 3));
                yield return null;
            }
            laserLine.gameObject.SetActive(false);
            coroutine = null;
        }
        private void PulseLaser()
        {
            Vector2 origin = new(transform.position.x, transform.position.y + skillData.len / 2);
            Vector2 boxSize = new(skillData.size, skillData.len);
            var hits = Physics2D.BoxCastAll(origin, boxSize, 0, Vector2.up, 0, LayerMask.GetMask("Enemy"));
            if (hits.Length > 0)
                foreach (var hit in hits)
                    if (hit.collider.TryGetComponent<IDamageable>(out var obj))
                        obj.TakeDamage(this);
        }
        protected override void UpgradeStat()
        {
            previewLine.startWidth = previewLine.endWidth = skillData.size * 1.25f;
        }
        public void AfterHit() { }
    }
}