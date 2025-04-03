using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Enemy : MonoBehaviour, IGameObject<ObjectData>
        {
            private SpriteRenderer spriteRenderer;
            private EnemyMovement movement;

            public void Awake()
            {
                movement = GetComponent<EnemyMovement>();
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            public void SetData(ObjectData data)
            {
                spriteRenderer.sprite = data.metaData.sprite;
                spriteRenderer.color = data.metaData.color;
                movement.StarMoving(data);
            }
        }
    }
}