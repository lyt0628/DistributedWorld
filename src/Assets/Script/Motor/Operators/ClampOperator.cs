


using GameLib.DI;
using QS.Api.Setting;
using QS.GameLib.Rx.Relay;

namespace QS.Motor
{
    class ClampOperator : AbstractOperator<MotorResult, MotorResult>
    {
        [Injected]
        readonly IGlobalPhysicSetting physicSetting;

        protected override MotorResult OperateAsync(MotorResult t)
        {
            t.displacement = MotorUtil.ClampDisplacement(t.position, t.displacement, physicSetting.ErrorTolerance);
            return t;
        }
    }
}