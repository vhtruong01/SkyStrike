using System.Collections.Generic;
using System.Linq;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyPhaseDataObserver : IData
        {
            private HashSet<IEnemyActionDataObserver> moveDataList;
            private HashSet<IEnemyActionDataObserver> fireDataList;

            public EnemyPhaseDataObserver()
            {
                moveDataList = new();
                fireDataList = new();
            }
            public IEnemyActionDataObserver[] GetActionDataArray(EActionType eActionType)
            {
                return eActionType switch
                {
                    EActionType.Move => moveDataList.ToArray(),
                    EActionType.Fire => fireDataList.ToArray(),
                    _ => null,
                };
            }
            private HashSet<IEnemyActionDataObserver> GetActionData(EActionType eActionType)
            {
                return eActionType switch
                {
                    EActionType.Move => moveDataList,
                    EActionType.Fire => fireDataList,
                    _ => null,
                };
            }
            public IEnemyActionDataObserver AddAction(EActionType eActionType)
            {
                var actionData = CreateAction(eActionType);
                if (actionData != null)
                {
                    var dataList = GetActionData(eActionType);
                    dataList.Add(actionData);
                }
                return actionData;
            }
            private IEnemyActionDataObserver CreateAction(EActionType eActionType)
            {
                return eActionType switch
                {
                    EActionType.Move => new EnemyMoveDataObserver(),
                    EActionType.Fire => new EnemyFireDataObserver(),
                    _ => null,
                };
            }
            public EnemyPhaseDataObserver Clone()
            {
                EnemyPhaseDataObserver newPhase = new();
                //
                return newPhase;
            }
        }
    }
}