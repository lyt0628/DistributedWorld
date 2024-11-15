


namespace GameLib
{
    interface IState : IIterable<IState>
    {
        bool Enter();
        void Exit();
    }
}