using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : ActionDataObserver, IEditorData<FireData, ActionDataObserver>
        {
            public FireDataObserver() : base()
            {

            }
            public override ActionDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
            public FireData ToGameData()
            {
                FireData fireData = new();
                return fireData;
            }
        }
    }
}