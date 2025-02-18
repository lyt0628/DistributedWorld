
using GameLib.DI;
using QS.Api.Chara.Instruction;
using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Api.Skill.Domain.Instruction;
using QS.Api.Skill.Predefine;
using QS.Chara.Domain;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using QS.Skill.SimpleSkill;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Skill.Handler
{
    /// <summary>
    /// The simple Skill is driven by Animaion Event. When <see cref="IMessage"/> was boardcast 
    /// by <see cref="IAnimAware"/>, the handler will trigger the animation and all stages will 
    /// come though Animation Event Message, which is predefined.
    /// 一個簡單技能實例只能處理一種技能
    /// </summary>
     class SimpleSkillAblity 
        : Ability, ISimpleSkillAbility
    {
        [Injected]
        readonly ISimpleSkillAnimCfg animCfg;
        [Injected]
        readonly ISubHandlerRegistry handlerRegistry;
        public UnityEvent OnPrecastCallbacks = new();
        public UnityEvent OnCastingCallbacks = new();
        public UnityEvent OnPostcastCallbacks = new();
        public UnityEvent OnShutdownCallbacks = new();
        public UnityEvent OnCancelCallbacks = new();

        readonly List<ISimpleSkillSubHandler> subHandlers = new();

        public ISimpleSkill Skill { get; }

        public virtual SimpleSkillStage CurrentStage { get; protected set; } = SimpleSkillStage.Shutdown;

        readonly ISkillKey key;

        /// <summary>
        /// 只有需要被使用的時候才會被創建，因此直接在構造器中
        /// 編寫資源加載邏輯即可。和常規的類是一樣的
        /// 
        /// 连续技在设计中是由简单技能复合而成的。如何链接这个过程
        /// 显然应该使用链表这个数据结构然后由第一个技能触发后一个技能
        /// 合理的方式也是用子处理器实现，缺点是领域的问题。
        /// 这样子无法体现连续技这个模型，但或许不错呢
        /// </summary>
        /// <param name="character"></param>
        /// <param name="skill"></param>
        public SimpleSkillAblity(Character character,ISimpleSkill skill) : base(character)
        {
            SkillGlobal.Instance.DI.Inject(this);

            Skill = skill;
            key = skill.Key;
            foreach (var h in skill.Handlers)
            {
                var handler = handlerRegistry.GetSubHandler(h);
                AddSubHandler(handler);
                handler.PreLoad(character, this);
            }

        }
        protected virtual void OnPrecast(IMessage m)
        {
            CurrentStage = SimpleSkillStage.Precast;
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Precast), OnPrecast);
            subHandlers.ForEach(h => h.OnPrecast(Character, this));
            OnPrecastCallbacks.Invoke();
        }

        protected virtual void OnCasting(IMessage m) {
            CurrentStage = SimpleSkillStage.Casting;
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Casting), OnCasting);
            subHandlers.ForEach(h => h.OnCasting(Character, this));
            OnCastingCallbacks.Invoke();
        }

        protected virtual void OnPostcast(IMessage m) {
            CurrentStage = SimpleSkillStage.Postcast;
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Postcast), OnPostcast);
            subHandlers.ForEach(h => h.OnPostcast(Character, this));
            OnPostcastCallbacks.Invoke();
        }

        protected virtual void OnShutdown(IMessage m)
        {
            CurrentStage = SimpleSkillStage.Shutdown;
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Shutdown), OnShutdown);
            subHandlers.ForEach(h => h.OnShutdown(Character, this));
            OnShutdownCallbacks.Invoke();
        }
        protected void AddListener()
        {
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.Precast), OnPrecast);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.Casting), OnCasting);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.Postcast), OnPostcast);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.Shutdown), OnShutdown);
        }

        protected void RemoveListener()
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Precast), OnPostcast);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Casting), OnCasting);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Postcast), OnPostcast);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.Shutdown), OnShutdown);
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(msg is ICastSkillInstr skillInstr)
            {
                if (skillInstr.Skill.Key != key)
                {
                    context.Write(msg);
                }
                OnInstructed();
                if(CurrentStage == SimpleSkillStage.Shutdown)
                {
                    Cast();
                }
            }
            // 先不考虑打断
            //else if (ReflectionUtil.IsChildOf<IInjuredInstr>(msg))
            //{
            //    Debug.Log("Combat, skill interrupted");
            //    RemoveListener();
            //    Debug.Log("[SimpleSkillHandler] TODO:Cleanup");
            //}

            context.Write(msg);
        }

        protected virtual void OnInstructed()
        {
            subHandlers.ForEach(h => h.OnInstructed(Character, this));
            
        }

        public void AddSubHandler(ISimpleSkillSubHandler subHandler)
        {
            subHandlers.Add(subHandler);
        }

        public T GetSubHandler<T>() where T : ISimpleSkillSubHandler
        {
            return (T)subHandlers.Find(h=>h is T);
        }

        public virtual void Cast()
        {
            if (Character.TryGetComponent<Animator>(out var anim))
            {
                var trigger = animCfg.GetAnimTrigger(key);
                anim.SetTrigger(trigger);
                Debug.Log(trigger);
            }
            RemoveListener(); // Remove Previous Listeners
            AddListener();
        }



        public virtual void Cancel()
        {
            RemoveListener();
            subHandlers.ForEach(h => h.OnCancel(Character, this));
            OnCancelCallbacks.Invoke();
            CurrentStage = SimpleSkillStage.Shutdown;
        }
    }
}