using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class EditorTool : MonoBehaviour
    {
        [SerializeField] private Controller controller;
        [Header("Level")]
        [SerializeField] private Button levelInfoBtn;
        [SerializeField] private Button playGameBtn;
        [SerializeField] private Button reviewWaveBtn;
        [SerializeField] private Button saveLevelBtn;
        [SerializeField] private Button newLevelBtn;
        [SerializeField] private Button openLevelBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private LevelMenu levelMenu;
        [Header("Object")]
        [SerializeField] private Button copyBtn;
        [SerializeField] private Button pasteBtn;
        [SerializeField] private Button removeBtn;
        [SerializeField] private Button cutBtn;
        [SerializeField] private Button editMovementBtn;
        [SerializeField] private Button bulletEditorBtn;
        [SerializeField] private BulletEditor bulletEditor;
        [SerializeField] private PathMenu pathMenu;
        private ObjectDataObserver tempItemData;
        private ObjectDataObserver curObjectDataObserver;

        public void Awake()
        {
            copyBtn.interactable = removeBtn.interactable = cutBtn.interactable = pasteBtn.interactable = editMovementBtn.interactable = false;
            copyBtn.onClick.AddListener(Copy);
            pasteBtn.onClick.AddListener(Paste);
            removeBtn.onClick.AddListener(Remove);
            cutBtn.onClick.AddListener(Cut);
            playGameBtn.onClick.AddListener(TestGame);
            levelInfoBtn.onClick.AddListener(levelMenu.Show);
            bulletEditorBtn.onClick.AddListener(bulletEditor.Show);
            editMovementBtn.onClick.AddListener(pathMenu.Show);
            saveLevelBtn.onClick.AddListener(SaveLevel);
            exitBtn.onClick.AddListener(Application.Quit);
            EventManager.onSelectObject.AddListener(SelectObject);
        }
        private void SelectObject(ObjectDataObserver data)
        {
            curObjectDataObserver = data;
            copyBtn.interactable = removeBtn.interactable = cutBtn.interactable = editMovementBtn.interactable = curObjectDataObserver != null;
        }
        protected void Copy()
        {
            tempItemData = curObjectDataObserver;
            if (!pasteBtn.interactable && tempItemData != null)
                pasteBtn.interactable = true;
        }
        protected void Paste()
        {
            EventManager.CreateObject(tempItemData.Clone());
        }
        protected void Remove()
        {
            if (curObjectDataObserver != null)
            {
                EventManager.RemoveObject(curObjectDataObserver);
                curObjectDataObserver = null;
                removeBtn.interactable = cutBtn.interactable = false;
            }
        }
        protected void Cut()
        {
            Copy();
            Remove();
        }
        private void SaveLevel()
            => controller.SaveLevelData();
        private void TestGame()
        {
            SaveLevel();
            GameManager.PlayGame();
        }
    }
}