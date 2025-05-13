using SkyStrike.Game;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.UI
{
    public enum EUIType
    {
        DmgText,
        HpBar,
        Notification,

    }
    public class UIPoolManager : MonoBehaviour
    {
        [Serializable]
        private class Visualizer
        {
            public EUIType uiType;
            public UIElement prefab;
            public Transform container;
        }

        [SerializeField] private List<Visualizer> visualizers;
        private Dictionary<EUIType, ObjectPool<UIElement>> poolDict;

        private void Awake()
        {
            poolDict = new();
            for (int i = 0; i < visualizers.Count; i++)
            {
                var visualizer = visualizers[i];
                poolDict.Add(visualizer.uiType, new(() => CreateObject(visualizer), Get, Release));
            }
        }
        private void Get(UIElement element)
            => element.gameObject.SetActive(true);
        private void Release(UIElement element)
            => element.gameObject.SetActive(false);
        private void OnEnable()
            => EventManager.Subscribe<UIEventData>(Display);
        private void OnDisable()
            => EventManager.Unsubscribe<UIEventData>(Display);
        private UIElement CreateObject(Visualizer visualizer)
        {
            var ui = Instantiate(visualizer.prefab, visualizer.container, false);
            ui.onDestroy = poolDict[visualizer.uiType].Release;
            return ui;
        }
        private void Display(UIEventData eventData)
            => poolDict[eventData.uiType].Get().Display(eventData);
    }
}