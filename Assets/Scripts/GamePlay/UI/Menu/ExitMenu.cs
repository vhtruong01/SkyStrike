using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class ExitMenu : Menu
        {
            [SerializeField] private Button cancelBtn;
            [SerializeField] private Button exitBtn;

            public override void Awake()
            {
                base.Awake();
                disappearTime = 0.5f;
                cancelBtn.onClick.AddListener(Collapse);
                exitBtn.onClick.AddListener(() => Application.Quit());
            }
        }
    }
}