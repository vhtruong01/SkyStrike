using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Enemy : Entity
        {
            [SerializeField] private SpriteRenderer spriteRenderer;

            public override IEnumerator Appear()
            {
                if (objectData.delay > 0)
                {
                    gameObject.SetActive(false);
                    yield return new WaitForSeconds(objectData.delay);
                    gameObject.SetActive(true);
                }
                print("done");
            }
            public override void Die()
            {
            }
            public override void SetData(IGameData data)
            {
                objectData = data as ObjectData;
                phaseData = objectData.phase;
                transform.position = objectData.position.Get();
                transform.localScale = objectData.scale.Get();
                var metaData = EventManager.GetMetaData(objectData.metaId) as MetaData;
                spriteRenderer.color = metaData.color;
                spriteRenderer.sprite = metaData.sprite;
            }
        }
    }
}