


using UnityEngine;

namespace QS.Common.Util
{
    public static class AnimationUtil
    {
        public static void RebindAnimatorController(Animator animator, RuntimeAnimatorController controller)
        {
            animator.runtimeAnimatorController = controller;
            animator.Rebind();
        }
    }
}