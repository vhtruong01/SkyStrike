using System;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Item : MonoBehaviour, IItem
        {
            private SpriteRenderer spriteRenderer;
            private Rigidbody2D rig;
            private BoxCollider2D col;
            private Animator animator;
            private ItemData itemData;
            public int quantity => itemData.quantity;
            public EItem type => itemData.type;

            public void Awake()
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                rig = GetComponent<Rigidbody2D>();
                col = GetComponent<BoxCollider2D>();
                animator = GetComponent<Animator>();
            }
            public void SetData(ItemData data)
            {
                itemData = data;
                spriteRenderer.sprite = data.sprite;
                gameObject.name = data.name;
                col.size = data.sprite.bounds.size;
                rig.linearVelocity = new Vector2(data.velocity.x, -data.velocity.y);
                animator.SetTrigger(Enum.GetName(data.animationType.GetType(), data.animationType));
            }
        }
    }
}