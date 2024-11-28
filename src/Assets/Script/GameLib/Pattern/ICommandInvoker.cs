namespace QS.GameLib.Pattern
{
    interface ICommandInvoker
    {
        void Invork(ICommand command);
        void Revoke(ICommand command);
        void Clear();
    }
}