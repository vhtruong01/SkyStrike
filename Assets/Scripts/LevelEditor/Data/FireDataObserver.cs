using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : ActionDataObserver, IEditorData<FireData, ActionDataObserver>
        {
            public FireDataObserver() : base()
            {
                //
            }
            public FireDataObserver(FireData fireData) : this() => ImportData(fireData);
            public override ActionDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
            public FireData ExportData()
            {
                FireData fireData = new();
                //
                return fireData;
            }

            public void ImportData(FireData data)
            {
                //
            }
        }
    }
}