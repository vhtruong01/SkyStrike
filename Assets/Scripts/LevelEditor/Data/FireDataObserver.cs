using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : ICloneable<FireDataObserver>
        {
            public FireDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
            public IGameData ToGameData()
            {
                FireData fireData = new();
                return fireData;
            }
        }
    }
}