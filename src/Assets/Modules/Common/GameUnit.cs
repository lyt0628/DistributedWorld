namespace QS.Common
{
    /// <summary>
    /// 游戏世界里的一个实体，怪兽，或是玩家
    /// </summary>
    public readonly struct GameUnit
    {
        public GameUnit(string id, UnitScope scope)
        {
            ID = id;
            Scope = scope;
        }

        /// <summary>
        /// 单位的唯一ID，全局唯一或本地唯一
        /// 如果是全局唯一，那就是数据库ID，或是远端生成的UUID，本地唯一就是本地生成的UUID
        /// </summary>
        public string ID { get; }

        public UnitScope Scope { get; }
    }


    public enum UnitScope
    {
        /// <summary>
        /// 本地唯一
        /// </summary>
        Local,

        /// <summary>
        /// 全局唯一
        /// </summary>
        Application
    }
}