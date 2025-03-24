

using QS.Chara.Abilities;
using QS.Common.FSM;

namespace QS.Chara
{
    public abstract class CharaStateBase : IState<CharaState>
    {
        public abstract CharaState State { get; }
        public CharaControlTemplate ControlFSM { get; }

        protected CharaStateBase(CharaControlTemplate controlFSM)
        {
            this.ControlFSM = controlFSM;

        }

        /// <summary>
        /// ��״̬�Լ�ѡ�����ʱ�䣬InPlace����ѡ��Update��
        /// ����������ѡ��OnAnimationMove.
        /// ����IK�����ǲ������õĲ��֣������״̬���Լ�����
        /// </summary>
        public virtual ProcessTime ProcessTime => ProcessTime.Update;

        public virtual ITransition<CharaState>[] Transitions => ITransition<CharaState>.Empty;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Process()
        {
            ControlFSM.ApplyDefaultDosplacement();
            ControlFSM.ApplyDefaultRotation();
        }
    }
}