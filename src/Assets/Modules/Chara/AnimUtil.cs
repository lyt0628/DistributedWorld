


using UnityEngine;

namespace QS.Chara
{
    public static class AnimUtil
    {
        public const int RELATIVE_DIR_F = 0;
        public const int RELATIVE_DIR_B = 1;
        public const int RELATIVE_DIR_L = 2;
        public const int RELATIVE_DIR_R = 3;

        /// <summary>
        /// 只考虑水平方位的受击
        /// </summary>
        /// <param name="attackDir"></param>
        /// <returns></returns>
        public static float CrossInput2AttackDir(Vector3 attackDir)
        {
            attackDir = attackDir.normalized;
            float min = 0f;
            float max = 1f;
            float t = 0f;

            var fwd = attackDir.z;
            var right = attackDir.x;
            // 来自左前方的攻击
            if (fwd >= 0 && right < 0)
            {
                min = 0;
                max = 1f;
                t = fwd / right;
            }
            // 来自右前方的攻击
            else if (fwd >= 0 && right >= 0)
            {
                min = 1f;
                max = 2f;
                t = right / fwd;
            }
            // 来自右后方的攻击
            else if (fwd < 0 && right >= 0)
            {
                min = 2f;
                max = 3f;
                t = fwd / right;
            }
            // 来自左后方的攻击
            else if (fwd < 0 && right < 0)
            {
                min = 3f;
                max = 4f;
                t = right / fwd;
            }

            return Mathf.Lerp(min, max, Mathf.Abs(t));
        }

        public static int CountMainMoveDir(Vector2 input)
        {
            if (Mathf.Approximately(input.magnitude, 0f))
            {
                return RELATIVE_DIR_B;
            }
            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                if (input.y > 0)
                {
                    return RELATIVE_DIR_F;
                }
                else
                {
                    return RELATIVE_DIR_B;
                }
            }
            else
            {
                if (input.x < 0)
                {
                    return RELATIVE_DIR_L;
                }
                else
                {
                    return RELATIVE_DIR_R;
                }
            }

        }

    }
}