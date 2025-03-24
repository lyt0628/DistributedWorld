using QS.Chara.Domain;

namespace QS.Player
{
    /// <summary>
    /// ���ڴ������ȫ�����ݵ�ģ��
    /// Ĭ�����ֵ�������ڱ�ʾ�������
    /// </summary>
    class Player : IPlayer
    {
        /// <summary>
        /// ��ǰ�������ʹ�õĽ�ɫ
        /// </summary>
        private Character activeChara;

        /// <summary>
        /// ����Ľ�ɫ�л��Ļص�
        /// </summary>
        public ActiveCharacterChangedEvent CharacterChanged { get; } = new();

        public Character ActiveChara
        {
            get { return activeChara; }
            set
            {
                var oldChara = activeChara;
                activeChara = value;
                CharacterChanged.Invoke(value, oldChara);
            }
        }

        /// <summary>
        /// ��ҳ��еĻ�������
        /// </summary>
        public int CoinCount { get; set; }
    }
}