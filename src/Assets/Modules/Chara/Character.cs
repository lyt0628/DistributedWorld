using QS.Api.Executor.Domain;
using QS.Api.WorldItem.Domain;
using QS.Combat;
using QS.Common;
using QS.GameLib.Pattern.Message;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace QS.Chara.Domain
{

    public class CharaAnimMsg : UnityEvent<string> { }



    /// <summary>
    /// 遊戲很複雜，我總不覺得自己能做好。但是，不去做，誰也不知道最後效果怎麼樣
    /// 技能和角色控制完全分开了
    /// 角色类里面要定义角色能干什么，如果是按ECS 来开 角色位于下层，
    /// 但是相反，现在用黑板模式，反而这个类要在上层或者根本就在一个包中
    /// NPC和角色作为独立的两个类来定义
    /// 
    /// 
    /// Character 怎么构造呢？是要用类定义还是用建造者来建造
    /// 
    /// Chara 是一个组合模块，这里边会定义耦合的领域模型。
    /// 一些上层的设计主要在这里进行约束
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        public abstract GameUnit Unit { get; }
        /// <summary>
        /// 角色使用黑板模式增加能力
        /// </summary>
        public IHandlerGroup HandlerGroup => m_HanderGroup;
        IHandlerGroup m_HanderGroup;

        /// <summary>
        /// 谁来负责这个指令转换规则的添加。两个大用户Agent和Chara，Agent不需要转换指令
        /// Chara的所有东西都可以外部添加，但是。
        /// 但是，唯独子类继承自它才不会损失面向对象的性质
        /// </summary>
        public IInstructionConverter InstructionConverter => m_InstructionConverter;
        IInstructionConverter m_InstructionConverter;

        /// <summary>
        /// 角色向外发送消息来提供自己的状态变化
        /// </summary>
        public IMessager Messager { get; } = new Messager();
        public IInstructionFiler InstrFiler => m_InstrFiler;
        FilterHandler m_InstrFiler;

        public virtual bool Frozen { get; set; } = false;
        /// <summary>
        /// 向外界递交动画回调接受到的状态
        /// </summary>
        public CharaAnimMsg OnAnimMsg { get; } = new();
        /// <summary>
        /// 角色一定有状态控制
        /// </summary>
        public CharaControlTemplate ControlFSM { get; private set; }
        /// <summary>
        /// 角色一定有战斗能力，没有战斗能力的NPC单独设计。
        /// </summary>
        public CombatorBehaviour Combator { get; private set; }
        /// <summary>
        /// 角色一定拥有动画能力
        /// </summary>
        public Animator Animator { get; private set; }
        
        /// <summary>
        /// 角色持有武器，切换武器依靠命令模式
        /// 角色知道武器怎么装配上去吗，角色不知道.这个挂载有ControlFSM负责，
        /// 这个挂载与动画强相关，不可能拆出来，技能的触发的主要阶段也是依赖于动画
        /// 动画+RootMotion不能说CastSkill 就立马Cast出来的这。可没办法，必须有依赖者自己检测被依赖者的状态。
        /// 酌情放弃指令的处理。Monster 拥有和Player一样的Weapon
        /// </summary>
        public IWeapon ActiveWeapon { get; }


        /// <summary>
        /// 这个方法是提供给动画状态机的方法
        /// 是实现的一部分
        /// </summary>
        /// <param name="param"></param>
        public void AnimAware(string param)
        {
            OnAnimMsg.Invoke(param);
            Messager.Boardcast(param, UnitMsg.Instance);
        }

        protected virtual void Start()
        {
            ControlFSM = GetComponent<CharaControlTemplate>();
            Combator = GetComponent<CombatorBehaviour>();
            Animator = GetComponent<Animator>();
            m_InstrFiler = CharaGlobal.Instance.GetInstance<FilterHandler>();
            m_HanderGroup = CharaGlobal.Instance.GetInstance<IHandlerGroup>();
            m_InstructionConverter = CharaGlobal.Instance.GetInstance<IInstructionConverter>();

            Combator.OnDead.AddListener(() =>
            {
                Messager.Boardcast(CharaConstants.CHARA_DIE, IMessage.Unit);
                var msgBus = CharaGlobal.Instance.GetInstance<IMessager>(QS.Common.DINames.MsgBus);
                msgBus.Boardcast(CharaConstants.CHARA_DIE, new DeadMsg(Unit));
            });
        }


        public virtual void Execute(IInstruction instruction)
        {
            if (Frozen) return;
            // 先转换，再审查
            instruction = InstructionConverter.Convert(instruction);
            // 通过黑名单来过滤指令，实现无敌效果
            if (m_InstrFiler.TryHande(instruction)) return;
        


            // 专家系统来处理指令
            if (ControlFSM.TryHande(instruction)) return;
            if (Combator.TryHande(instruction)) return;
            if (m_HanderGroup.TryHande(instruction)) return;

            Debug.Log($"Insturction is miss to handle {instruction.GetType().Name}");
        }

    }
}
