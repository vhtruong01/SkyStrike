using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class WorldFrontier : MonoBehaviour
        {
            public void OnCollisionEnter2D(Collision2D collision)
            {
                print(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}