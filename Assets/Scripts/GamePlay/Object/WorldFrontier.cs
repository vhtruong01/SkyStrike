using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class WorldFrontier : MonoBehaviour
        {
            public void OnCollisionEnter2D(Collision2D collision)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}