using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionDataGroupObserver : ICloneable<ActionDataGroupObserver>
        {
            private MoveDataObserver moveAction;
            private FireDataObserver fireAction;

            public ActionDataGroupObserver()
            {
                moveAction = new();
                fireAction = new();
            }
            public ActionDataGroupObserver Clone()
            {
                return new()
                {
                    fireAction = fireAction.Clone(),
                    moveAction = moveAction.Clone()
                };
            }
            public IEditorData GetActionData(EActionType actionType)
            {
                return actionType switch
                {
                    EActionType.Move => moveAction,
                    EActionType.Fire => fireAction,
                    _ => null,
                };
            }

            public IGameData ToGameData()
            {
                ActionGroupData actionGroupData = new();
                actionGroupData.moveData = moveAction.ToGameData() as MoveData;
                actionGroupData.fireData = fireAction.ToGameData() as FireData;
                return actionGroupData;
            }
        }
    }
}