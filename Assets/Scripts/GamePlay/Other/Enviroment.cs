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
        private void OnEnable()
        {
            EventManager.Subscribe(EEventType.PrepareGame, DisplayIntro);
            EventManager.Subscribe(EEventType.StartGame, Enable);
        }
        private void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.PrepareGame, DisplayIntro);
            EventManager.Unsubscribe(EEventType.StartGame, Enable);
        }
        private void DisplayIntro()
            => planet.transform.DOScale(0, 1.5f).onKill = () => planet.gameObject.SetActive(false);
        private void Disable()
        {
            foreach (ParticleSystem p in environments)
                p.Stop();
        }
        private void Enable()
        {
            foreach (ParticleSystem p in environments)
                p.Play();
        }
    }
}