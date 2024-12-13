
using GameLib.DI;
using QS.Api.Character.Instruction;
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

        readonly string skillNo;
        readonly string skillName;
        Action PrecastEnterCallbacks;
        Action PrecastExitCallbacks;
        Action CastingEnterCallbacks;
        Action CastingExitCallbacks;
        Action PostcastEnterCallbacks;
        Action PostcastExitCallbacks;

        void PrecastEnter(IMessage m) => PrecastEnterCallbacks?.Invoke();
        void PrecastExit(IMessage m) => PrecastExitCallbacks?.Invoke();
        void CastingEnter(IMessage m) => CastingEnterCallbacks?.Invoke();
        void CastingExit(IMessage m) => CastingExitCallbacks?.Invoke();
        void PostcastEnter(IMessage m) => PostcastEnterCallbacks?.Invoke();
        void PostcastExit(IMessage m) => PostcastExitCallbacks?.Invoke();
        public SimpleSkillAblity(Character character ,string skillNo, string skillName)
            :base(character)
        {
            this.skillNo = skillNo;
            this.skillName = skillName;
            PrecastEnterCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastEnter), PrecastEnter);
            PrecastExitCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastExit), PrecastExit);
            CastingEnterCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingEnter), CastingEnter);
            CastingExitCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingExit), CastingExit);
            PostcastEnterCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastEnter), PostcastEnter);
            PostcastExitCallbacks = ()=> Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastExit), PostcastExit);
            SkillGlobal.Instance.DI.Inject(this);
        }

        private void AddListener()
        {
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastEnter), PrecastEnter);
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastExit), PrecastExit);
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingEnter), CastingEnter);
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingExit), CastingExit);
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastEnter), PostcastEnter);
            Character.Messager.AddListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastExit), PostcastExit);
        }

        private void RemoveListener()
        {
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastEnter), PrecastEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PrecastExit), PrecastExit);
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingEnter), CastingEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.CastingExit), CastingExit);
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastEnter), PostcastEnter);
            Character.Messager.RemoveListener(animCfg.GetMsg(skillNo, skillName, SimpleSkillStage.PostcastExit), PostcastExit);

        }

        public void AddSubHandler(SimpleSkillStage stage, Action subHandler)
        {

            switch (stage)
            {
                case SimpleSkillStage.PrecastEnter:
                    PrecastEnterCallbacks += subHandler;
                    break;
                case SimpleSkillStage.PrecastExit:
                    PrecastExitCallbacks += subHandler;
                    break;
                case SimpleSkillStage.CastingEnter:
                    CastingEnterCallbacks += subHandler;
                    break;
                case SimpleSkillStage.CastingExit:
                    CastingExitCallbacks += subHandler;
                    break;
                case SimpleSkillStage.PostcastEnter:
                    PostcastEnterCallbacks += subHandler;
                    break;
                case SimpleSkillStage.PostcastExit:
                    PostcastExitCallbacks += subHandler;
                    break;
                default:
                    break;
            }
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(ReflectionUtil.IsChildOf<ISimpleSkillInstr>(msg))
            {
                Debug.Log("simple skill triggered");
                AddListener();
                if (Character.TryGetComponent<Animator>(out var anim))
                {
                    anim.SetTrigger(animCfg.GetTrigger(skillNo, skillName)) ;
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
    }
}