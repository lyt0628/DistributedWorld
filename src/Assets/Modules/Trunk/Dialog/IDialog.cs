
using System.Collections.Generic;

namespace QS.Dialog
{
    /// <summary>
    /// һ�ν����Ի�����֧���û��ǲ��ɼ���
    /// </summary>
    public interface IDialog : IEnumerable<IDialogLine>
    {
        /// <summary>
        /// �����Ի���ǰ������
        /// </summary>
        bool CanOpen { get; }
        /// <summary>
        /// �ǲ���һ��Ŀ��ֻ�ᴥ��һ�εĶԻ�
        /// </summary>
        bool IsOneShot { get; }

        void AddBranch(IDialog branch);
        void AddLine(IDialogLine line);

        /// <summary>
        /// �Ի��ǲ����Ѿ�����������һ����,������Բ�����������
        /// </summary>
        //bool IsDone { get; }



    }

}