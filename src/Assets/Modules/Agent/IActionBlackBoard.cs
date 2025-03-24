

namespace QS.Agent
{
    public interface IActionBlackBoard
    {
        int Count { get; }
        int Capacity { get; set; }
        IAIAction ActiveAction { get; }

        void Clear();
        void Execute();
        bool HasActiveAction();
        bool TryAdd(IAIAction action);
    }
}