


using UnityEngine;

namespace QS.Api.Common.Util.Asset
{
    /// <summary>
    /// IAsset 是对资源的抽象, 
    /// 它屏蔽数据来源, 可以是Resources也可以是AssetBundle
    /// 可以执行数据的持久化, 我们使用一个确定的Key就可以拿到一个Asset
    /// Asset 统一管理的资源, 屏蔽了项目对资源的管理
    /// 游戏定义,是通过保存在ActiveRecord中, 不需要持久化
    /// 游戏仓库不是Executor, 它应该做持久化, 物体是通过继承属性完成了所有的数据, 所以简单使用Toml的持久化Api就可以
    /// 完成处理了
    /// 玩家状态比较复杂, 具体的说, 一个玩家状态包括有
    /// Combat的数据: CombatData, 实时状态, 此外还应当持久化定义状态, 比方说
    /// CombatData初始值 由 Profession 和 Lv 决定 CombatData的初始值, 应当通过这些属性来进行查找
    /// //TODO: 如果有一些特殊道具可以并非预定义的增强这些初始属性, 应当使用命令模式进行处理.大概率不会有
    /// (Combat不保持这些初始值, 如果有人需要知道这些初始值, 应当到ActiveRecord进行查找)
    /// 最终是被谁使用??? 如果使用Repository, 应当由Repository来做持久化
    /// 每个模块各自做各自的持久化, 持久化同样的是使用Instruction
    /// 比方说, Combat 
    /// 游戏状态的持久化方式是, 
    /// Asset要提供什么Api呢?
    /// 
    /// 在 与本地的序列化 的时候使用Toml, 在与网络进行通信的时候 使用Protobuf
    /// </summary>
    public interface IAsset
    {
        /// <summary>
        /// 字符串资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
        /// <summary>
        /// 整型资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetInt(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        float GetFloat(string key);
        GameObject GetPrefab(string key);
        Sprite GetSprite(string key);
        string GetFileContent(string key);
        string GetLuaScript(string key);
        AudioClip GetAudioClip(string key);
        AnimationClip GetAnimationClip(string key);

    }
}