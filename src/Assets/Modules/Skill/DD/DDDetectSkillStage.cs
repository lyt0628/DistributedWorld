

using QS.Api.Common.Util.Detector;

using QS.Api.Skill.Domain;
using System;
using UnityEngine;

namespace QS.Skill
{

    sealed class DDDetectSkillState : DetectSkillState
    {
        readonly SkillStage stage;
        readonly Func<IDetector> detectorProvider;
        readonly Action BeforeDetectCB;
        readonly Action<GameObject[]> OnEndDetectCB;
        readonly Action<GameObject[], GameObject[]> OnDetectedPerFrameCB;
        readonly Action OnInterruptCB;
        readonly bool m_Interruptable;
        public DDDetectSkillState(BaseSkill skill,
                                  SkillStage stage,
                                  Func<IDetector> detectorProvider,
                                  Action beforeDetectCB,
                                  Action<GameObject[], GameObject[]> onDetectedPerFrameCB,
                                  Action<GameObject[]> onEndDetectCB,
                                  bool interruptable,
                                  Action onInterruptCB) : base(skill)
        {
            this.stage = stage;
            this.detectorProvider = detectorProvider ?? throw new ArgumentNullException(nameof(detectorProvider));
            BeforeDetectCB = beforeDetectCB;
            OnEndDetectCB = onEndDetectCB;
            OnDetectedPerFrameCB = onDetectedPerFrameCB;
            OnInterruptCB = onInterruptCB;
            m_Interruptable = interruptable;
        }

        public override SkillStage State => stage;

        protected override IDetector Detector => detectorProvider();

        protected override void BeginDetect() => BeforeDetectCB?.Invoke();

        protected override void OnEndDetect(GameObject[] targetSet) => OnEndDetectCB?.Invoke(targetSet);

        protected override void OnDetectedPerFrame(GameObject[] targets, GameObject[] newTargets) => OnDetectedPerFrameCB?.Invoke(targets, newTargets);

        public override void OnInterrupt()
        {
            base.OnInterrupt();
            OnInterruptCB?.Invoke();
        }

        public override bool Interrutable => m_Interruptable;
    }

}