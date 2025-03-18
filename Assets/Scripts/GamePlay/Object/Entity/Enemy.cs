using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [RequireComponent(typeof(PhaseManager))]
        public class Enemy : Entity
        {
            [SerializeField] private SpriteRenderer spriteRenderer;
            private PhaseManager phaseManager;

            public void Awake()
            {
                phaseManager = GetComponent<PhaseManager>();
            }
            public override void SetData(IGameData data)
            {
                isDie = false;
                objectData = data as ObjectData;
                phaseData = objectData.phase;
                transform.position = new(objectData.position.x, objectData.position.y, transform.position.z);
                transform.localScale = new(objectData.scale.x, objectData.scale.y, transform.localScale.z);
                var metaData = EventManager.GetMetaData(objectData.metaId) as MetaData;
                spriteRenderer.color = metaData.color;
                spriteRenderer.sprite = metaData.sprite;
            }
            public override IEnumerator Appear()
            {
                if (objectData.delay > 0)
                {
                    gameObject.SetActive(false);
                    yield return new WaitForSeconds(objectData.delay);
                }
                gameObject.SetActive(true);
                yield return StartCoroutine(phaseManager.Test(phaseData));
                if (!isDie)
                {
                    Die();
                    EventManager.RemoveObject(this);
                }
                print("done");
            }
            public override void Die()
            {
            }
        }
    }
}