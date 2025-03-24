using System.Collections;
using System.Collections.Generic;

namespace QS.Dialog
{
    class Dialog : IDialog
    {
        /// <summary>
        /// ��δ�����֧��ֱ�߾���
        /// </summary>
        readonly List<IDialogLine> m_Lines = new();
        /// <summary>
        /// ���ܵķ�֧�Ի�
        /// </summary>
        readonly List<IDialog> m_Branches = new();
        public void AddLine(IDialogLine line)
        {
            m_Lines.Add(line);
        }

        public void AddBranch(IDialog branch)
        {
            m_Branches.Add(branch);
        }

        public bool CanOpen { get; }

        public bool IsOneShot { get; }

        class DialogEnumerator : IEnumerator<IDialogLine>
        {

            public IDialogLine Current => throw new System.NotImplementedException();

            object IEnumerator.Current => throw new System.NotImplementedException();

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new System.NotImplementedException();
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }
        }


        public IEnumerator<IDialogLine> GetEnumerator()
        {
            return ((IEnumerable<IDialogLine>)m_Lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_Lines).GetEnumerator();
        }


    }
}