

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Api.WorldItem.Domain;
using QS.Chara.Domain;
using QS.Combat;
using QS.Common;
using QS.Trunk;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace QS.Chara
{
    /// <summary>
    /// 面对这种离身的物件，按照面向对象的思想，应该新建立一个MonoBehaviour
    /// </summary>

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(CapsuleCollider))]
    class Arrow : MonoBehaviour
    {
        IInstruction[] instrs;
        Rigidbody rigid;
        [Injected]
        readonly IHitInstr hitInstr;
        private void Start()
        {
            TrunkGlobal.Instance.Context.Inject(this);
            rigid = GetComponent<Rigidbody>();
        }
        public void Shoot(Vector3 direction, GameObject shooter, params IInstruction[] instrs)
        {
            Debug.Log(direction);
            this.instrs = instrs;
            transform.parent = null;
            // 歪就歪吧
            //direction = transform.TransformDirection(Vector3.down);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            rigid.velocity = 10f * direction;
            //Destroy(gameObject);
            //enabled = false;
            var selfCollider = GetComponent<Collider>();
            var ignoredColliders = shooter.GetComponentsInChildren<Collider>();
            foreach (var c in ignoredColliders)
            {
                Physics.IgnoreCollision(selfCollider, c);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Collision Happen {collision.collider.name}");
            hitInstr.AttackDir = collision.contacts[0].normal;
            hitInstr.AttackForce = 0.3f;
            hitInstr.HitStopTime = .3f;
       
            if (collision.collider.TryGetComponent<Character>(out var executor))
            {
                if (instrs != null)
                {
                    foreach (var i in instrs)
                    {
                        Debug.Log(i);
                        executor.Execute(i);
                    }
                }
                executor.Execute(hitInstr);
            }
        }
    }
}