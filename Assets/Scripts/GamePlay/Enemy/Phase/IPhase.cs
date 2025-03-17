using System.Collections;

namespace SkyStrike
{
    namespace Enemy
    {
        public interface IPhase
        {
            public IEnumerator StartAction();
            public void SetCoroutine(ICoroutine couroutine);
        }
    }
}