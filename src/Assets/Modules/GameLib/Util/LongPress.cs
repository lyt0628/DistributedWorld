

using UnityEngine;

namespace QS.GameLib.Util
{
    /// <summary>
    /// 检测输入的布尔值是否在已经保持了一段时间为真
    /// </summary>
    public class LongPress
    {
        bool pressed = false;
        readonly float duration = 0f;
        float startTime = 0f;
        public LongPress(float duration)
        {
            this.duration = duration;
        }

        public bool Input(bool t)
        {
            //Debug.Log($"t: {t}");
            if (!t)
            {
                pressed = false;
                return false;
            }

            if (t && !pressed)
            {
                //Debug.Log($"Long Press: Start");
                startTime = Time.time;
                pressed = true;
                return false;
            }
            //Debug.Log($"Long Press: {Time.time - startTime}");
            return Time.time - startTime >= duration;

        }
    }
}