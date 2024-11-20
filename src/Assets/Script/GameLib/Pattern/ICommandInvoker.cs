
using GameLib.Pattern;

namespace GameLib.Pattern
{
    interface ICommandInvoker
    {
        void Invork(ICommand command);
        void Revoke(ICommand command);
        void Clear();
    }
}