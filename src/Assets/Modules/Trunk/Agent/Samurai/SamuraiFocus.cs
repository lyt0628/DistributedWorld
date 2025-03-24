//using GameLib.DI;
//using QS.Chara;
//using QS.Common.Util;
//using UnityEngine;

//namespace QS.Agent
//{
//    public class SamuraiFocus : AgentStateBase
//    {

//        public override AgentState State => AgentState.Focus;

//        [Injected]
//        readonly IAimLockInstr aimLockInstr;
//        public SamuraiFocus(SamuraiAgent agent) : base(agent)
//        {
//            aimLockInstr = AgentGlobal.Instance.GetInstance<IAimLockInstr>();
//        }
//        public override void Enter()
//        {
//            base.Enter();
//            aimLockInstr.Target = agent.Sensor.Enemy;
//            agent.Chara.Execute(aimLockInstr);
//        }
//        // 孙子状态用ifelse 放Lua实现
//        public override void Process()
//        {
//            var randomVal = Random.Range(0, 100);
//            if (!agent.Sensor.EnemyFound) return;

//            if (!TransformUtil.IsInSectorArea(agent.transform, agent.Sensor.Enemy, Common.RelativeDir.Forward, 100, 2.5f))
//            {
//                agent.Actions.TryAdd(new ApprochAction(agent, agent.Sensor.Enemy));
//            }
//            else if (agent.Actions.ActiveAction is KatanaLightAttackAction)
//            {
//                if (randomVal < 95)
//                {
//                    agent.Actions.TryAdd(new RecedeAction(agent, agent.Sensor.Enemy));
//                }
//                else if (Time.time - agent.Actions.ActiveAction.ExecutedTime < 0.75)
//                {
//                    agent.Actions.TryAdd(new KatanaLightAttackAction(agent, agent.Sensor.Enemy));
//                }
//            }
//            else if (randomVal < 80)
//            {
//                agent.Actions.Clear();
//                agent.Actions.TryAdd(new KatanaLightAttackAction(agent, agent.Sensor.Enemy));
//            }



//        }
//    }
//}