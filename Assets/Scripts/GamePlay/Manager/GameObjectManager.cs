using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike
{
    namespace Game
    {
        public class GameObjectManager : MonoBehaviour
        {
            [SerializeField] private GameObject prefab;
            private ObjectPool<IGameObject> pool;

            public void Awake()
            {
                pool = new(CreateObject);
                EventManager.onRemoveObject.AddListener(RemoveItem);
            }
            private IGameObject CreateObject()
            {
                var item = (Instantiate(prefab, transform, false).GetComponent<IGameObject>())
                    ?? throw new Exception("wrong prefab type");
                item.gameObject.name = prefab.name;
                return item;
            }
            public IGameObject CreateItem(IGameData data)
            {
                var item = pool.Get();
                item.SetData(data);
                item.gameObject.SetActive(true);
                return item;
            }
            public void RemoveItem(IGameObject item)
            {
                item.gameObject.SetActive(false);
                pool.Release(item);
            }
        }
    }
}