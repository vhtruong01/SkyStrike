using UnityEngine;

namespace SkyStrike.Game
{
    public interface IInput
    {
        public Vector2 Direction { get; }
        public void Active();
        public void Deactive();
    }
    public abstract class Input : MonoBehaviour, IInput
    {
        public abstract Vector2 Direction { get; }
        public abstract void Active();
        public abstract void Deactive();
    }
}