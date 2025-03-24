

using QS.Api.Common.Util.Detector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Skill
{
    public abstract class DetectSkillState : MsgSwitchSkillState
    {
        public DetectSkillState(BaseSkill skill) : base(skill)
        {
        }
        readonly HashSet<GameObject> targetSet = new();
        protected abstract IDetector Detector { get; }

        protected virtual void BeginDetect() { }
        protected virtual void BeforeDetectPerFrame() { }
        protected virtual void OnDetectedPerFrame(GameObject[] target, GameObject[] newTargets) { }
        protected virtual void OnEndDetect(GameObject[] targetSet) { }


        public override void Process()
        {
            BeforeDetectPerFrame();
            var targets = Detector.Detect();
            var newTargets = targets.Except(targetSet);
            OnDetectedPerFrame(targets, newTargets.ToArray());
            targetSet.UnionWith(targets);
        }

        public override void Enter()
        {
            base.Enter();
            targetSet.Clear();
            BeginDetect();
        }

        public override void Exit()
        {
            OnEndDetect(targetSet.ToArray());
            targetSet.Clear();
            base.Exit();
        }
    }
}