using System.Collections;

namespace SkyStrike
{
    namespace Game
    {
        public interface IGameObject : IObject
        {
            public ObjectData data { get; }
            public void SetData(ObjectData data);
            public IEnumerator Appear();
        }
    }
}