


using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Executor.Domain.Handler
{
    class MoveInstructionAnimHandler : InBoundPipelineHandlerAdapter
    {
        public class Msg
        {
           public float speed;
           public bool jump;
           public bool jumping;
           public bool inAir = false;
        }
        readonly Animator _animator;
        public MoveInstructionAnimHandler(Animator animator)
        {
            _animator = animator;
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            Msg m = (Msg)msg;
            _animator.SetFloat("Speed", m.speed);
           
            if (m.jump)
            {
                _animator.SetTrigger("Jump");
            }
            else if (m.inAir)
            {
                _animator.SetTrigger("FreeFall");
            }
            else
            {
                _animator.SetTrigger("Grounded");
            }
        }
    }
}