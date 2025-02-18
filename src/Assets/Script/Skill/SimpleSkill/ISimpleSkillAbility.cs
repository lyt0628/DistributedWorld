

using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.Skill.SimpleSkill;
using System;

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// 按照配置文件裏定義的，SimpleSkill可以做這麼一些事情
    /// 1. 觸發動畫，DefaultSimpleSkill 是基於動畫回調的，這個能力是基礎，由主處理器操作
    /// 2. 在技能的不同階段實例化預製體到掛載點上
    /// 3. 在技能的不同階段播放音效
    /// 4. 在技能的不同階段調用Lua腳本
    /// 5. 
    /// 這些能力都得先提供一個處理器才行
    /// </summary>
     interface ISimpleSkillAbility : IInstructionHandler
    {
        ISimpleSkill Skill { get; }
        SimpleSkillStage CurrentStage { get; }
        void Cast();
        void Cancel();
        void AddSubHandler(ISimpleSkillSubHandler subHandler);
        T GetSubHandler<T>() where T : ISimpleSkillSubHandler;

    }
}