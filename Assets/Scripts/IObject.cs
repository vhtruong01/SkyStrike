using UnityEngine;

namespace SkyStrike
{
    public interface IObject
    {
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Transform transform { get; }
        public GameObject gameObject { get; }
        public bool isActive => gameObject.activeSelf;
    }
}