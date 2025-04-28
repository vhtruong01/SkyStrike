using DG.Tweening;
using UnityEngine;

namespace SkyStrike.Game
{
    public class Enviroment : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer planet;
        private ParticleSystem[] environments;

        public void Awake()
        {
            environments = GetComponentsInChildren<ParticleSystem>();
            planet.transform.position = Vector3.zero;
            planet.transform.localScale = new(20, 20, 20);
            planet.gameObject.SetActive(true);
            Disable();
        }
        public void DisplayIntro(float duration)
        {
            planet.transform.DOScale(0, duration).onKill = () => planet.gameObject.SetActive(false);
        }
        public void Disable()
        {
            foreach (ParticleSystem p in environments)
                p.Stop();
        }
        public void Enable()
        {
            foreach (ParticleSystem p in environments)
                p.Play();
        }
    }
}