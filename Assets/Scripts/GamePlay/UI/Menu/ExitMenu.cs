using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class ExitMenu : Menu
    {
        [SerializeField] private Button cancelBtn;
        [SerializeField] private Button exitBtn;

        public override void Awake()
        {
            base.Awake();
            cancelBtn.onClick.AddListener(Collapse);
            exitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}