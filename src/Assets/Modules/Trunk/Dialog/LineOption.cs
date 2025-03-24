

namespace QS.Dialog
{
    /// <summary>
    /// 玩家可选的一句对话，有前置条件
    /// </summary>
    public class LineOption : ILine
    {
        /// <summary>
        /// 只是当前这句台词是否可用
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