using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.UI
{
    public class DamageVisualizer : MonoBehaviour
    {
        private static readonly float displayTime = 0.2f;
        private static readonly float startSize = 1.5f;
        private TextMeshProUGUI text;
        private WaitForSeconds waitForSeconds;
        public UnityAction<DamageVisualizer> onDestroy { get; set; }

        public void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            waitForSeconds = new WaitForSeconds(0.2f);
        }
        public void SetData(Vector2 pos, int damage, Color c)
        {
            transform.position = pos.SetZ(transform.position.z);
            text.text = damage.ToString();
            text.color = c;
            gameObject.SetActive(true);
            StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            float totalTime = displayTime;
            while (totalTime > 0)
            {
                totalTime -= Time.unscaledDeltaTime;
                transform.localScale = Vector3.one * (totalTime / displayTime * startSize + 1f);
                yield return null;
            }
            yield return waitForSeconds;
            gameObject.SetActive(false);
            onDestroy.Invoke(this);
        }
    }
}