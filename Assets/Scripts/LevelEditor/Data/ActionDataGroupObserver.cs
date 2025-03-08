namespace SkyStrike
{
    namespace Editor
    {
        public class ActionDataGroupObserver : ICloneable<ActionDataGroupObserver>
        {
            private MoveDataObserver moveAction;
            private FireDataObserver fireAction;

            public void AddActionData(EActionType actionType)
            {
                switch (actionType)
                {
                    case EActionType.Move:
                        moveAction ??= new();
                        break;
                    case EActionType.Fire:
                        fireAction ??= new();
                        break;
                }
            }
            public void RemoveActionData(EActionType actionType)
            {
                switch (actionType)
                {
                    case EActionType.Move:
                        moveAction = null;
                        break;
                    case EActionType.Fire:
                        fireAction = null;
                        break;
                }
            }
            public ActionDataGroupObserver Clone()
            {
                ActionDataGroupObserver actionData = new ();
                actionData.fireAction = fireAction?.Clone();
                actionData.moveAction = moveAction?.Clone();
                return actionData;
            }
            public IData GetActionData(EActionType actionType)
            {
                return actionType switch
                {
                    EActionType.Move => moveAction,
                    EActionType.Fire => fireAction,
                    _ => null,
                };
            }
            public bool IsEmpty() => moveAction == null & fireAction == null;
        }
    }
}