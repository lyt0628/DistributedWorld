
using System.Collections.Generic;

namespace QS.Dialog
{
    /// <summary>
    /// 一段交互对话。分支对用户是不可见的
    /// </summary>
    public interface IDialog : IEnumerable<IDialogLine>
    {
        /// <summary>
        /// 开启对话的前提条件
        /// </summary>
        bool CanOpen { get; }
        /// <summary>
        /// 是不是一周目中只会触发一次的对话
        /// </summary>
        bool IsOneShot { get; }

        void AddBranch(IDialog branch);
        void AddLine(IDialogLine line);

        /// <summary>
        /// 对话是不是已经触发过至少一次了,这个属性不是这个管理的
        /// </summary>
        //bool IsDone { get; }



    }

}