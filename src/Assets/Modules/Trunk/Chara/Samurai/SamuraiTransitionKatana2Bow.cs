

using QS.Chara.Abilities;
using QS.Common.FSM;
using QS.Common.Util;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Chara
{
    class SamuraiTransitionKatana2Bow : ICharaStateTransition
    {

        public SamuraiTransitionKatana2Bow(CharaControlTemplate controlFSM)
        {
            this.controlFSM = controlFSM;
            this.katanaModel = GameObjectUtil.FindChild(controlFSM.transform, "Weapon_r").gameObject;
            this.bowModel = GameObjectUtil.FindChild(controlFSM.transform, "SM_Bow_02");
            bowTargetArmed = GameObjectUtil.FindChild(controlFSM.transform, "bow");
            Assert.IsNotNull(bowTargetArmed);
        }
        readonly GameObject katanaModel;
        readonly Transform bowModel;
        readonly Transform bowTargetArmed;

        readonly CharaControlTemplate controlFSM;
        public ProcessTime ProcessTime => ProcessTime.Update;

        public Func<bool> Condition => () => false;

        public CharaState Target => CharaState.Idle;

        public void Begin()
        {
            controlFSM.Chara.Animator.SetInteger("Weapon", 2);
        }

        public bool Transite()
        {
            var animState = controlFSM.Chara.Animator.GetCurrentAnimatorStateInfo(0);


            if (animState.IsName("GhostSamurai_Bow_Common_Equip_Root")
                && animState.normalizedTime > 0.90f)
            {
                return true;
            }

            if (katanaModel != null && animState.IsName("GhostSamurai_APose_Unarm_1_Root")
            &&animState.normalizedTime > 0.65f)
            {
                GameObject.DestroyImmediate(katanaModel);
            }

            if (animState.IsName("GhostSamurai_Bow_Common_Equip_Root")
                && animState.normalizedTime > 0.20f)
            {
                //bowModel
                if (bowModel.parent != bowTargetArmed)
                {
                    bowModel.parent = bowTargetArmed;
                    //bowModel.localPosition = Vector3.zero;
                    bowModel.localRotation = Quaternion.Euler(-90, 0,0);
                }
            }

            return false;
        }
    }
}