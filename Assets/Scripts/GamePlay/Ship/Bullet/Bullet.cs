using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Ship
    {
        public class Bullet : MonoBehaviour
        {
            private float timeLife;
            public UnityEvent<Bullet> onDestroy { get; private set; }

            public void Awake()
            {
                onDestroy = new();
            }
            public void Update()
            {
                if (timeLife <= 0)
                {
                    Disapear();
                    return;
                }
                timeLife -= Time.deltaTime;
                transform.Translate(10 * Time.deltaTime * Vector3.up);
            }
            public void Hit()
            {
            }
            public void Disapear() => onDestroy.Invoke(this);
            public void SetTimeLife(float time) => timeLife = time;
            public void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("WorldCollider"))
                    Disapear();
            }
            public void OnCollisionEnter2D(Collision2D collision)
            {
                print(collision);
            }
        }
    }
}