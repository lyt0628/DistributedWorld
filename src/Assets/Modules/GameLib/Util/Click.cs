

using UnityEngine;

namespace QS.GameLib.Util
{
    /// <summary>
    /// 检测一个布尔值是否在很短时间内就由真切换为假
    /// </summary>
    public class Click
    {
        bool pressed = false;
        readonly float maxDuration = 0f;
        float startTime = 0f;
        public Click(float maxDuration)
        {
            this.maxDuration = maxDuration;
        }

        public bool Input(bool t)
        {
            if (t && !pressed)
            {
                pressed = true;
                startTime = Time.time;
                return false;
            }
            if (t && pressed)
            {
                return false;
            }
            if (!t && pressed)
            {
                pressed = false;
                //Debug.Log($"{Time.time - startTime}");
                return Time.time - startTime < maxDuration;
            }
            return false;
        }
    }
}