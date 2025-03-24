

namespace QS.Dialog
{
    /// <summary>
    /// ��ҿ�ѡ��һ��Ի�����ǰ������
    /// </summary>
    public class LineOption : ILine
    {
        /// <summary>
        /// ֻ�ǵ�ǰ���̨���Ƿ����
        /// </summary>
        public bool Avaliable { get; private set; } = true;

        //public int Id => m_line.Id;

        public string Text => m_line.Text;

        readonly ILine m_line;

        public LineOption(ILine line)
        {
            m_line = line;
        }
    }
}