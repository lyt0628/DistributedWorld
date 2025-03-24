

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;
using UnityEngine.Events;

namespace QS.Skill
{

    /// <summary>
    /// �����������Ǵ����ܵ���������,����������״̬��������ָ˳��ִ�е�
    /// ��������Ϊ����״̬����Ҫɳ�й�����Դ
    /// 
    /// ����ֻ�Ǽ̳���FSM �͸����������������Բ��㹻�������հ�����ֵ�����һ���ְ�
    /// </summary>
    public abstract class BaseSkill : FSM<SkillStage>
    {
        protected BaseSkill(Character chara)
        {
            Chara = chara != null ? chara : throw new ArgumentNullException(nameof(chara));
            Chara.ControlFSM.BeforeHit.AddListener(OnInterrupt);
            Chara.OnAnimMsg.AddListener(OnAnimMsg);
        }

        void OnAnimMsg(string msg)
        {
            if(GetState(CurrentState) is BaseSkillState skillState)
            {
                skillState.OnAnimMsg(msg);
            }
        }

        public Character Chara { get; }
        public bool Casting => CurrentState != SkillStage.Recoveried;

        /// <summary>
        /// ��������Ҫ��������ʱ��Ļص�
        /// </summary>
        public UnityEvent OnTryCast { get; } = new();

        public override SkillStage DefaultState => SkillStage.Recoveried;

        public virtual void OnInterrupt()
        {
            if (!Casting) return;
            if(GetState(CurrentState) is not BaseSkillState skillState) return;
            if (!skillState.Interrutable) return;


            /// ״̬�Ǿ����ִ���ߣ��жϵ�����Ӧ����״̬������
            skillState.OnInterrupt();
            SwitchTo(SkillStage.Recoveried);
        }


        protected virtual void OnPlayerHint() { }
        public override void Handle(IInstruction instruction)
        {
            OnPlayerHint();
            OnTryCast.Invoke();
            if (!Casting)
            {
                SwitchTo(SkillStage.Precast);
            }
        }

        public void NextStage()
        {
            var nextStage = CurrentState switch
            {
                SkillStage.Precast => SkillStage.Casting,
                SkillStage.Casting => SkillStage.Postcast,
                SkillStage.Postcast => SkillStage.Shutdown,
                SkillStage.Shutdown => SkillStage.Recoveried,
                _ => SkillStage.Recoveried,
            };
            SwitchTo(nextStage);
        }
    }

}
