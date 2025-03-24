


using QS.Api.Common.Util.Detector;
using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Skill
{
    class SkillStageFactory : ISKillStageFactory
    {
        public IState<SkillStage> Detect(BaseSkill skill,
                                  SkillStage stage,
                                  Func<IDetector> detectorProvider,
                                  Action beforeDetectCB = null,
                                  Action<GameObject[], GameObject[]> onDetectedPerFrameCB = null,
                                  Action<GameObject[]> onEndDetectCB = null,
                                  bool interruptable = false,
                                  Action onInterruptCB = null)
        {
            return new DDDetectSkillState( skill,
                                           stage,
                                           detectorProvider,
                                           beforeDetectCB ,
                                           onDetectedPerFrameCB,
                                           onEndDetectCB,
                                           interruptable,
                                           onInterruptCB);
        }

        public IState<SkillStage> MsgSwitch(BaseSkill skill,
                                    SkillStage state,
                                    string animTrigger = "",
                                    string animNext = "SK_Next",
                                    Action onEnter = null,
                                    Action onProcess = null,
                                    Action onExit = null,
                                    bool interruptable = false,
                                    Action onInterruptCB = null)
        {
            return new DDMsgSwithSkillState( skill,
                                     state,
                                     animTrigger,
                                     animNext,
                                     onEnter,
                                     onProcess,
                                     onExit,
                                     interruptable,
                                     onInterruptCB);
        }

        public IState<SkillStage> Unit()
        {
            return IState<SkillStage>.Unit;
        }
    }
}