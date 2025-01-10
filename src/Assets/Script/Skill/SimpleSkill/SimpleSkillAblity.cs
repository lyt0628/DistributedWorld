
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

namespace QS.Skill.Handler
{
    /// <summary>
    /// The simple Skill is driven by Animaion Event. When <see cref="IMessage"/> was boardcast 
    /// by <see cref="IAnimAware"/>, the handler will trigger the animation and all stages will 
    /// come though Animation Event Message, which is predefined.
    /// 一個簡單技能實例只能處理一種技能
    /// </summary>
     class SimpleSkillAblity 
        : Ablity, ISimpleSkillHandler
    {
        [Injected]
        readonly ISimpleSkillAnimCfg animCfg;
        [Injected]
        readonly ISubHandlerRegistry handlerRegistry;

        readonly List<ISimpleSkillSubHandler> subHandlers = new();

        public ISimpleSkill Skill { get; }
        readonly ISkillKey key;

        /// <summary>
        /// 只有需要被使用的時候才會被創建，因此直接在構造器中
        /// 編寫資源加載邏輯即可。和常規的類是一樣的
        /// </summary>
        /// <param name="character"></param>
        /// <param name="skill"></param>

        public SimpleSkillAblity(Character character,ISimpleSkill skill)
            : base(character)
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


        void PrecastEnter(IMessage m)
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastEnter), PrecastEnter);
            subHandlers.ForEach(h => h.OnPrecastEnter(Character, this));
        }
        void PrecastExit(IMessage m)
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastExit), PrecastExit);
            subHandlers.ForEach(h=>h.OnPrecastExit(Character, this));
        }
        void CastingEnter(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingEnter), CastingEnter);
            subHandlers.ForEach(h => h.OnCastingEnter(Character, this));
        }
        void CastingExit(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingExit), CastingExit);
            subHandlers.ForEach(h => h.OnCastingExit(Character, this));
        }
        void PostcastEnter(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastEnter), PostcastEnter);
            subHandlers.ForEach(h => h.OnPostcastEnter(Character, this));
        }
        void PostcastExit(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastExit), PostcastExit);
            subHandlers.ForEach(h => h.OnPostcastExit(Character, this));
        }

        private void AddListener()
        {
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastEnter), PrecastEnter);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastExit), PrecastExit);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.CastingEnter), CastingEnter);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.CastingExit), CastingExit);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastEnter), PostcastEnter);
            Character.Messager.AddListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastExit), PostcastExit);
        }

        private void RemoveListener()
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastEnter), PrecastEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastExit), PrecastExit);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingEnter), CastingEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingExit), CastingExit);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastEnter), PostcastEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastExit), PostcastExit);

        }


        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(msg is ISimpleSkillInstr skillInstr)
            {
                
                if (skillInstr.Skill.Key != key) {
                   context.Write(msg);
                }

                RemoveListener(); // Remove Previous Listeners
                AddListener();
                if (Character.TryGetComponent<Animator>(out var anim))
                {
                    var trigger = animCfg.GetAnimTrigger(key);
                    anim.SetTrigger(trigger);
                    //Debug.Log(trigger);
                }
            }
            else if (ReflectionUtil.IsChildOf<IInjuredInstr>(msg))
            {
                Debug.Log("Injured, skill interrupted");
                RemoveListener();
                Debug.Log("[SimpleSkillHandler] TODO:Cleanup");
            }

            context.Write(msg);
        }

        public void AddSubHandler(ISimpleSkillSubHandler subHandler)
        {
            subHandlers.Add(subHandler);
        }

        public T GetSubHandler<T>() where T : ISimpleSkillSubHandler
        {
            return (T)subHandlers.Find(h=>h is T);
        }


    }
}