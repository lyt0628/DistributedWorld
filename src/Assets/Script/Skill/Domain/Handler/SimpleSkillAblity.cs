
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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Skill.Handler
{
    /// <summary>
    /// The simple Skill is driven by Animaion Event. When <see cref="IMessage"/> was boardcast 
    /// by <see cref="IAnimAware"/>, the handler will trigger the animation and all stages will 
    /// come though Animation Event Message, which is predefined.
    /// </summary>
    public class SimpleSkillAblity 
        : Ablity, ISimpleSkillHandler
    {
        [Injected]
        readonly ISimpleSkillAnimCfg animCfg;

        readonly List<ISimpleSkillSubHandler> subHandlers = new();

        readonly ISkillKey key;

        void PrecastEnter(IMessage m)
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastEnter), PrecastEnter);
            subHandlers.ForEach(h => h.OnPrecastEnter(Character));
        }
        void PrecastExit(IMessage m)
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PrecastExit), PrecastExit);
            subHandlers.ForEach(h=>h.OnPrecastExit(Character));
        }
        void CastingEnter(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingEnter), CastingEnter);
            subHandlers.ForEach(h => h.OnCastingEnter(Character));
        }
        void CastingExit(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.CastingExit), CastingExit);
            subHandlers.ForEach(h => h.OnCastingExit(Character));
        }
        void PostcastEnter(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastEnter), PostcastEnter);
            subHandlers.ForEach(h => h.OnPostcastEnter(Character));
        }
        void PostcastExit(IMessage m) {
            Character.Messager.RemoveListener(animCfg.GetMsg(key, SimpleSkillStage.PostcastExit), PostcastExit);
            subHandlers.ForEach(h => h.OnPostcastExit(Character));
        }

        public SimpleSkillAblity(Character character, ISkillKey key)
            :base(character)
        {
            this.key = key;
            SkillGlobal.Instance.DI.Inject(this);
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
            if(ReflectionUtil.IsChildOf<ISimpleSkillInstr>(msg))
            {
                RemoveListener(); // Remove Previous Listeners
                AddListener();
                if (Character.TryGetComponent<Animator>(out var anim))
                {
                    anim.SetTrigger(animCfg.GetTrigger(key)) ;
                }
            }

            else if (ReflectionUtil.IsChildOf<IInjuredInstruction>(msg))
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
    }
}