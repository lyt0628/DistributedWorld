

using QS.Common;

namespace QS.Combat
{
    /// <summary>
    /// 战斗系统，屏蔽远端和本地计算的区别
    /// </summary>
    abstract class CombatSystem : ICombatSystem
    {

        public static CombatSystem Get(ServiceType type)
        {
            return type switch
            {
                ServiceType.Local => new LocalCombatSystem(),
                ServiceType.Remote => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };
        }

        /// <summary>
        /// 计算伤害的接口, 直接使用组件，还是拿接口.
        /// 这是基于游戏模型的，如果是基于角色的每个角色都有统一的技能树，那么这里就定义
        /// 这个。否则定义通用技能就好
        /// 总之，需要从 组件拿数据出来，在计算，返回。
        /// 
        /// 如果是本地计算的话，直接拿用户给的技能来计算了，
        /// 如果是远程的，就把技能代码传给远程计算
        /// </summary>
        public abstract void TakeDamage(CombatorBehaviour attacked, CombatorBehaviour attacker, ISkill skill);

        public abstract void TakeHeal(CombatorBehaviour combator, float hpDelta);


    }
}