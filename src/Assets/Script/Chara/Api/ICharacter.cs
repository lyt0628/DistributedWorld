using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.Api.Character
{
    /// <summary>
    /// 角色是游戏中的核心定义, 它表示领域而不是实现,
    /// 实现是由Executor 承担, Executor 不涉及领域, 因而可以轻松拓展
    /// 现在专注于描述领域 角色会使用技能,但技能本身与角色不耦合
    /// 技能只是一个特别的Handler能处理一些技能指令
    /// </summary>
    public interface ICharacter  : IRelayExecutor,  IMessagerProvider, IAnimAware
    {

    }

}
