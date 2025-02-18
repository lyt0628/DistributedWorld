

using QS.Api.Skill.Domain;

namespace QS.Api.Skill.Predefine
{
    /// <summary>
    /// DefaultSimpleSkill 的命名約定，這個名稱唯一就好，大家約定好。
    /// 根據技能的Key來生成.
    /// </summary>
    public interface ISimpleSkillAnimCfg
    {
        /// <summary>
        /// <para>>
        /// MSG_Format: SK[id]-[name]-[stage]-[patch]
        /// </para>
        /// <para>>
        /// The patch part may not exits. e.g. SK00001-FireBall-Precast.

        /// </para>
        /// </summary>
        /// <remarks>
        /// More Example:
        /// <list type="bullet">
        /// <item>SK00002-IceWave-Casting-LV20. The animation changed when Skill level above 20.</item>
        /// <item>SK00002-IceWave-Casting-V1. The animation changed once.</item>
        /// </list>
        /// </remarks>
        const string MSG_Format = "SK{0}_{1}_{2}";

        /// 如果技能有子階段，那麼每個子階段都視作一個技能處理，比方說 單手劍的普通攻擊 編號001 子階段1, 子階段不超過兩位
        /// SK00001_01_SwordTap
        const string Trigger_Format = "SK{0}_{1}";

        /// <summary>
        /// GetMsg GetMsg of a Create Skill
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        string GetMsg(ISkillKey key, SimpleSkillStage stage); // 先查配置文件, 没找到就生成默认约定好的名称

        string GetAnimTrigger(ISkillKey key);
            
    }
}
