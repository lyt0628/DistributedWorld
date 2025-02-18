

using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using QS.Skill.Handler;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// 庆祝，连续技的机制完成
    /// </summary>
    class PhasedSimpleSkillAbility : SimpleSkillAblity
    {
        bool canShufflePhase = false;
        bool shuffled = false;
        public PhasedSimpleSkillAbility(Character character, PhasedSimpleSkill phasedSkill) : base(character, phasedSkill)
        {
            phases = phasedSkill
                .Select(sk => new SimpleSkillAblity(character, sk))
                .ToList();
            foreach (var phase in phases)
            {
                phase.OnShutdownCallbacks.AddListener(() =>
                {
                    Character.Messager.RemoveListener("InputFrame", EnableShufflePhase);
                    canShufflePhase = false;
                    if (shuffled)
                    {
                        shuffled = false;
                        currentPhase++;
                        if (currentPhase == phases.Count)
                        {
                            currentPhase = 0;
                        }
                        Cast();
                    }
                    else
                    {
                        currentPhase = 0;
                    }
                    

                });
            }
        }
        public override SimpleSkillStage CurrentStage { get =>phases[currentPhase].CurrentStage; protected set{ } }
        readonly List<SimpleSkillAblity> phases;
        int currentPhase = 0;

        public override void Cast()
        {
            phases[currentPhase].Cast();
            Character.Messager.AddListener("InputFrame", EnableShufflePhase);
        }

        protected override void OnInstructed()
        {
            base.OnInstructed();
            if (!canShufflePhase) return;
            shuffled = true;

        }

        void EnableShufflePhase(IMessage _)
        {
            Debug.Log("Input");
            canShufflePhase = true;
        }
    }
}