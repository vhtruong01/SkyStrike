using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private TextMeshProUGUI text;

        public void Enable(bool isEnabled)
        {
            if (isEnabled)
            {
                btn.interactable = true;
                text.color = Color.cyan;
            }
            else
            {
                btn.interactable = false;
                text.color = Color.red;
            }
        }
    }
}