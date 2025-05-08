using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace SkyStrike.Game
{
    public enum ECameraEffect
    {
        None = 0,
        Shake,
    }
    public class CameraEffect : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        private Camera cam;
        private Volume volume;

        public void Awake()
        {
            cam = GetComponent<Camera>();
            volume = GetComponent<Volume>();
        }
        public void OnEnable()
        {
            EventManager.Subscribe(EEventType.SlowTime, SlowTime);
            EventManager.Subscribe(EEventType.ShakeScreen, Shake);
        }
        public void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.SlowTime, SlowTime);
            EventManager.Unsubscribe(EEventType.ShakeScreen, Shake);
        }
        public void Shake()
            => StartCoroutine(Shake_Enumerator());
        public IEnumerator Shake_Enumerator()
        {
            float duration = .5f;
            Vector3 pos = cam.transform.position;
            while (duration > 0)
            {
                duration -= Time.unscaledDeltaTime;
                transform.position = (0.15f * Random.insideUnitCircle).SetZ(pos.z);
                yield return null;
            }
            cam.transform.position = pos;
        }
        public void SlowTime()
            => StartCoroutine(SlowTime_Enumerator());
        public IEnumerator SlowTime_Enumerator()
        {
            float duration = 1f;
            float elapsedTime = 0;
            volume.profile.TryGet(out ColorAdjustments cA);
            cA.active = true;
            float delta;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                delta = elapsedTime / duration;
                Time.timeScale = Mathf.Clamp(1 - delta, 0.1f, 1);
                cA.hueShift.value = 180 * delta;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(5);
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                delta = elapsedTime / duration;
                Time.timeScale = Mathf.Clamp(delta, 0.1f, 1);
                cA.hueShift.value = 180 * (1 - delta);
                yield return null;
            }
            cA.active = false;
        }
    }
}