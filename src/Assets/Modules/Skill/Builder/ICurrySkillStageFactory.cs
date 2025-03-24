using QS.Api.Common.Util.Detector;
using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Skill
{

    /// <summary>
    /// 柯里化的技能阶段工厂
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public interface ICurrySkillStageFactory
    {
        IState<SkillStage> MsgSwitch(string animTrigger = "",
                                             string animNext = "SK_Next",
                                             Action onEnter = null,
                                             Action onProcess = null,
                                             Action onExit = null,
                                             bool interruptable = false,
                                             Action onInterruptCB = null);

        IState<SkillStage> Detect(Func<IDetector> detectorProvider,
                                  Action beforeDetectCB = null,
                                  Action<GameObject[], GameObject[]> onDetectedPerFrameCB = null,
                                  Action<GameObject[]> onEndDetectCB = null,
                                  bool interruptable = false,
                                  Action onInterruptCB = null);
        IState<SkillStage> Void();
    }
}