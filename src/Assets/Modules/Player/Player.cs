using QS.Chara.Domain;

namespace QS.Player
{
    /// <summary>
    /// 用于储存玩家全局数据的模块
    /// 默认名字的玩家用于表示本地玩家
    /// </summary>
    class Player : IPlayer
    {
        /// <summary>
        /// 当前玩家正在使用的角色
        /// </summary>
        private Character activeChara;

        /// <summary>
        /// 激活的角色切换的回调
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
        /// 玩家持有的货币数量
        /// </summary>
        public int CoinCount { get; set; }
    }
}