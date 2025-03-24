

using System.Collections;
using System.Collections.Generic;

namespace QS.Dialog
{
    /// <summary>
    /// 一些台词选项，需要在用户选择后作出一些行动
    /// </summary>
    public class LineOptionList : IEnumerable<ILine>
    {
        int id { get; }
        readonly List<LineOption> m_options = new();
        public int Result { get; private set; }
        public void SetResult(int result)
        {
            Result = result;
        }

        public void AddOption(LineOption option)
        {

            m_options.Add(option);
        }

        class LineOpetionEnumerator : IEnumerator<ILine>
        {
            readonly LineOptionList m_optionList;
            int index = -1;

            public LineOpetionEnumerator(LineOptionList optionList)
            {
                m_optionList = optionList;
            }

            public ILine Current => m_optionList.m_options[index];

            object IEnumerator.Current => m_optionList.m_options[index];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (index >= m_optionList.m_options.Count - 1) return false;
                index++;
                if (!m_optionList.m_options[index].Avaliable)
                {
                    return MoveNext();
                }
                else
                {
                    return true;
                }
            }

            public void Reset()
            {
                index = -1;
            }
        }


        public IEnumerator<ILine> GetEnumerator()
        {
            return new LineOpetionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LineOpetionEnumerator(this);
        }
    }
}