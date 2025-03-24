


using System.Collections;
using System.Collections.Generic;

namespace QS.Dialog
{
    /// <summary>
    /// 一段连续的台词，单人叙述
    /// </summary>
    class Section : IEnumerable<Line>
    {
        readonly Queue<Line> m_lines = new();
        public void AddLine(Line line)
        {
            m_lines.Enqueue(line);
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return ((IEnumerable<Line>)m_lines).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_lines).GetEnumerator();
        }
    }
}