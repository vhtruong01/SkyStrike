using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SkyStrike.Game
{
    public class CameraEffect : MonoBehaviour
    {
        private Camera cam;
        private Volume volume;
        private ColorAdjustments cA;
        private Vignette vignette;

        public void Awake()
        {
            cam = GetComponent<Camera>();
            volume = GetComponent<Volume>();
            volume.profile.TryGet(out cA);
            volume.profile.TryGet(out vignette);
            cA.active = false;
            vignette.active = false;
        }
        public void OnEnable()
        {
            EventManager.Subscribe(EEventType.SlowTime, SlowTime);
            EventManager.Subscribe(EEventType.ShakeScreen, Shake);
            EventManager.Subscribe(EEventType.StopTime, StopTime);
            EventManager.Subscribe(EEventType.CloseScene, CloseScene);
        }
        public void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.SlowTime, SlowTime);
            EventManager.Unsubscribe(EEventType.ShakeScreen, Shake);
            EventManager.Unsubscribe(EEventType.StopTime, StopTime);
            EventManager.Unsubscribe(EEventType.CloseScene, CloseScene);
            StopAllCoroutines();
            Time.timeScale = 1.0f;
        }
        private void Shake()
            => StartCoroutine(Shake_Enumerator());
        private void StopTime()
            => StartCoroutine(StopTime_Enumerator());
        private void SlowTime()
            => StartCoroutine(SlowTime_Enumerator());
        private void CloseScene()
            => StartCoroutine(CloseScene_Enumerator());
        private IEnumerator CloseScene_Enumerator()
        {
            float duration = 1f;
            float elapsedTime = 0;
            vignette.active = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = elapsedTime / duration;
                yield return null;
            }
        }
        private IEnumerator Shake_Enumerator()
        {
            float duration = .75f;
            Vector3 pos = cam.transform.position;
            while (duration > 0)
            {
                duration -= Time.unscaledDeltaTime;
                transform.position = (0.1f * Random.insideUnitCircle).SetZ(pos.z);
                yield return null;
            }
            cam.transform.position = pos;
        }
        private IEnumerator SlowTime_Enumerator()
        {
            float duration = 1f;
            float elapsedTime = 0;
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
            cA.hueShift.value = 0;
            cA.active = false;
        }
        private IEnumerator StopTime_Enumerator()
        {
            float duration = 0.25f;
            float elapsedTime = 0;
            cA.active = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                cA.saturation.value = -100 * elapsedTime / duration;
                yield return null;
            }
            Time.timeScale = 0.01f;
            yield return new WaitForSecondsRealtime(0.75f);
            Time.timeScale = 1;
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                cA.saturation.value = -100 * (1 - elapsedTime / duration);
                yield return null;
            }
            cA.saturation.value = 0;
            cA.active = false;
        }
    }
}