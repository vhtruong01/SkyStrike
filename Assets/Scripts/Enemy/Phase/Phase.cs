using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class Phase : IPhase
        {
            private List<IAction> actions;
            private ICoroutine coroutineParent;

            public Phase()
            {
                actions = new List<IAction>();
                var moveAct1 = new MoveAction();
                var attackAct1 = new AttackAction();
                moveAct1.nextAction = new AttackAction();
                actions.Add(new AttackAction());
                actions.Add(moveAct1);
                actions.Add(attackAct1);
            }
            public void SetCoroutine(ICoroutine coroutine)
            {
                coroutineParent = coroutine;
                for (int i = 0; i < actions.Count; i++)
                {
                    IAction action = actions[i];
                    while (action != null)
                    {
                        action.parent = coroutine.gameObject;
                        action = action.nextAction;
                    }
                }
            }
            public IEnumerator StartAction()
            {
                List<Coroutine> coroutines = new();
                do
                {
                    coroutines.Clear();
                    List<IAction> nextActions = new();
                    foreach (IAction curAction in actions)
                    {
                        Coroutine coroutine = coroutineParent.StartCoroutine(curAction.Invoke());
                        coroutines.Add(coroutine);
                        if (curAction.nextAction != null)
                            nextActions.Add(curAction.nextAction);
                    }
                    actions = nextActions;
                    for (int i = 0; i < coroutines.Count; i++)
                        yield return coroutines[i];
                } while (actions.Count > 0);
            }
        }
    }
}