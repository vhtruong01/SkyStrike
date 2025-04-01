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
            public override void SetData(ObjectData data)
            {
                isDie = false;
                objectData = data;
                //transform.position = new(objectData.position.x, objectData.position.y, transform.position.z);
                //transform.localScale = new(objectData.scale.x, objectData.scale.y, transform.localScale.z);
                var metaData = EventManager.GetMetaData(objectData.metaId) as MetaData;
                phaseData = null;
                spriteRenderer.color = metaData.color;
                spriteRenderer.sprite = metaData.sprite;
            }
            public override IEnumerator Appear()
            {
                if (objectData.delay > 0)
                    yield return new WaitForSeconds(objectData.delay);
                yield return StartCoroutine(phaseManager.Begin(phaseData));
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