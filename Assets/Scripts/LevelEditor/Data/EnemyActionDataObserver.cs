namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyActionDataObserver : ICloneable<EnemyActionDataObserver>
        {
            private EnemyMoveDataObserver moveAction;
            private EnemyFireDataObserver fireAction;
            public int index { get; set; }

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
            public EnemyActionDataObserver Clone()
            {
                EnemyActionDataObserver actionData = new ();
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