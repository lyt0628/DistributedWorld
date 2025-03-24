using QS.Api.Common.Util.Detector;
using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Skill
{
    /// <summary>
    /// 完整的技能阶段工厂
    /// </summary>
    public interface ISKillStageFactory
    {
       
        
        IState<SkillStage> Detect(BaseSkill skill,
                                  SkillStage stage,
                                  Func<IDetector> detectorProvider,
                                  Action beforeDetectCB = null,
                                  Action<GameObject[], GameObject[]> onDetectedPerFrameCB = null,
                                  Action<GameObject[]> onEndDetectCB = null,
                                  bool interruptable = false,
                                  Action onInterruptCB = null);
        IState<SkillStage> MsgSwitch(BaseSkill skill,
                                     SkillStage state,
                                     string animTrigger = "",
                                     string animNext = "SK_Next",
                                     Action onEnter = null,
                                     Action onProcess = null,
                                     Action onExit = null,
                                     bool interruptable = false,
                                     Action onInterruptCB = null);
        IState<SkillStage> Unit();
    }

}