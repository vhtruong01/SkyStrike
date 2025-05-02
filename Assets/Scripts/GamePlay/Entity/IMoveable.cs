using System.Collections;

namespace SkyStrike.Game
{
    public interface IMoveable : IEntityComponent
    {
        public IEnumerator Travel(float delay);
        public void Move();
        public void Stop();
    }
}