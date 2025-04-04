using QS.Api.Setting;

namespace QS.Impl.Setting
{

    public class GlobalPhysicSetting : IGlobalPhysicSetting
    {
        public float Gravity => -10f;

        public float ErrorTolerance => 0.01f;

        public float HalfErrorTolerance => ErrorTolerance / 2;
    }

}