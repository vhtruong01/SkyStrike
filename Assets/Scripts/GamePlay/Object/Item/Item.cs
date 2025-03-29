using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Item : MonoBehaviour, IItem
        {
            private Rigidbody2D rig;

            public void Awake()
            {
                rig = GetComponent<Rigidbody2D>();
            }
            public void Start()
            {
                rig.linearVelocity = Vector2.down;
            }
            public void OnCollisionEnter2D(Collision2D collision)
            {
                print(collision.gameObject.name);
            }
        }
    }
}