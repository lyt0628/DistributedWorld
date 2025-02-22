


using QS.GameLib.Rx.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Motor
{
    public class MotorFlow
    {

        readonly Relay<MotorResult> relay;

        MotorFlow(GameLib.Rx.Relay.IObservable<MotorResult> observable)
        {
            relay = Relay<MotorResult>.Of(observable);
        }

        MotorFlow(Relay<MotorResult> relay)
        {
            this.relay = relay;
        }

        public MotorFlow Clamp()
        {
            var op = new ClampOperator();
            MotorGlobal.Instance.DI.Inject(op);
            return new MotorFlow(relay.Operate(op));
        }

        public static MotorFlow Combine(IEnumerable<GameLib.Rx.Relay.IObservable<MotorResult>> motors)
        {
            var relay = Relay<MotorResult>.CombineLatest(motors, (results) =>
            {
                var result = new MotorResult
                {
                    position = ((MotorResult)results.First()).position,
                    displacement = Vector3.zero,
                    velocity = Vector3.zero
                };

                foreach (MotorResult r in results.Cast<MotorResult>())
                {
                    result.displacement += r.displacement;
                    result.velocity += r.velocity;
                }
                return result;
            });

            return new MotorFlow(relay);
        }

        //public static MotorFlow Grivity(Vector3 grivityDir, float grivityValue, Relay<GrivityInput> inputs)
        //{
        //    var op = new GrivityOperator(grivityDir, grivityValue);
        //    return new MotorFlow(inputs.Operate(op));
        //}
        public static IOperator<GrivityInput, MotorResult> Grivity(Vector3 grivityDir, float grivityValue, Relay<GrivityInput> inputs)
        {
            var op = new GrivityOperator(grivityDir, grivityValue);
            MotorGlobal.Instance.DI.Inject(op);
            inputs.Operate(op);
            return op;
        }

        public static IOperator<MoveInput, MotorResult> Move(Relay<MoveInput> inputs)
        {
            var op = new MoveOperator();
            inputs.Operate(op);
            return op;
        }

        public MotorFlow Subscrib(Action<MotorResult> onNext)
        {
            relay.Subscrib(onNext);
            return this;
        }
    }
}