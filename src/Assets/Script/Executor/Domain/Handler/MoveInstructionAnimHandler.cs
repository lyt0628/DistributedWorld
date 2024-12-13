


using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Executor.Domain.Handler
{
    class MoveInstructionAnimHandler : InBoundPipelineHandlerAdapter
    {
        public class Msg
        {
           public float speed;
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
        }
    }
}