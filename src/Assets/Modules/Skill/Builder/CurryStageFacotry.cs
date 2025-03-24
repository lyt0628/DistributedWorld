

using GameLib.DI;
using QS.Api.Common.Util.Detector;
using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Skill
{
    interface ISkillBuilderContext
    {
        BaseSkill Skill { get; }
        SkillStage CurrentStage { get; }
    }

    class CurryStageFacotry : ICurrySkillStageFactory
    {
        [Injected]
        readonly ISKillStageFactory stageFactory;

        readonly ISkillBuilderContext skillContext;

        public CurryStageFacotry(ISkillBuilderContext skillContext)
        {
            SkillGlobal.Instance.Inject(this);
            this.skillContext = skillContext;
        }

        public IState<SkillStage> Detect(Func<IDetector> detectorProvider,
                                         Action beforeDetectCB = null,
                                         Action<GameObject[], GameObject[]> onDetectedPerFrameCB = null,
                                         Action<GameObject[]> onEndDetectCB = null,
                                         bool interruptable = false,
                                         Action onInterruptCB = null)
        {
            return stageFactory.Detect(skillContext.Skill,
                                       skillContext.CurrentStage,
                                       detectorProvider,
                                       beforeDetectCB,
                                       onDetectedPerFrameCB ,
                                       onEndDetectCB,
                                       interruptable,
                                       onInterruptCB);
        }

        public IState<SkillStage> MsgSwitch(string animTrigger = "",
                                             string animNext = "SK_Next",
                                             Action onEnter = null,
                                             Action onProcess = null,
                                             Action onExit = null,
                                             bool interruptable = false,
                                             Action onInterruptCB = null)
        {
            return stageFactory.MsgSwitch(skillContext.Skill,
                                          skillContext.CurrentStage,
                                          animTrigger,
                                          animNext,
                                          onEnter,
                                          onProcess,
                                          onExit,
                                          interruptable,
                                          onInterruptCB);
        }


        public IState<SkillStage> Void()
        {
            return stageFactory.Unit();
        }
    }

}