namespace QS.GameLib.Pattern
{
    interface IState : IIterable<IState>
    {
        bool Enter();
        void Exit();
    }
}