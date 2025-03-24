

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Agent
{
    class ActionBlackBoard : IActionBlackBoard
    {


        public ActionBlackBoard()
        {
            ActiveAction = IAIAction.Unit;
        }
        public int Capacity { get; set; } = 10;

        readonly List<IAIAction> m_Actions = new();
        public IAIAction ActiveAction { get; protected set; }

        public int Count => m_Actions.Count;

        public bool TryAdd(IAIAction action)
        {
            if (m_Actions.Count < Capacity)
            {
                m_Actions.Add(action);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            m_Actions.Clear();
        }


        public void Execute()
        {
            UpdateActiveAction();
            //Debug.Log($"Action {ActiveAction.GetType().Name} Executed!");
            ActiveAction.Process();
        }

        private void UpdateActiveAction()
        {
            if (Time.time - ActiveAction.ExecutedTime <= ActiveAction.Span) return;

            ActiveAction.Exit();
            IAIAction act = IAIAction.Unit;
            while (!act.IsValid && m_Actions.Any())
            {
                act = m_Actions.OrderBy(a => a.Priority).First();
                if (act.IsValid)
                {
                    ActiveAction = act;
                    act.Enter();
                }
                m_Actions.RemoveAt(0);
            }
            ActiveAction = act.IsValid ? act : IAIAction.Unit;

        }

        public bool HasActiveAction()
        {
            return ActiveAction != IAIAction.Unit;
        }
    }
}