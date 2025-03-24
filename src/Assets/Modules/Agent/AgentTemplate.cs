using QS.Api.Executor.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;

namespace QS.Agent
{
    public abstract class AgentTemplate : FSMBehaviour<AgentState>
    {
        public override AgentState DefaultState => AgentState.Free;
        public ISensor Sensor { get;  set; }
        public ISteering Steering { get;  set; }
        public IActionBlackBoard Actions { get; } = new ActionBlackBoard();

        public Character Chara { get; private set; }

        public override bool CanHandle(IInstruction instruction)
        {
            return false;
        }

        public override void Handle(IInstruction instruction)
        {
        }

        protected virtual void Start()
        {
            Chara = GetComponent<Character>();
        }

        protected override void Update()
        {
            //Debug.Log(CurrentState);
            base.Update();
            Actions.Execute();
        }

    }
}