


namespace QS.GameLib.Util
{
    public static class MathUtil
    {
        public static float Clamp(float value, float min, float max)
        {
            float bound = max - min;
            float ret = value;
            while (ret > max)
            {
                ret -= bound;
            }
            while (ret < min)
            {
                ret += bound;
            }

            return ret;
        }

        private static int i = 0;
        public static string UUID()
        {
            i++;
            return i.ToString();
        }
    }
}