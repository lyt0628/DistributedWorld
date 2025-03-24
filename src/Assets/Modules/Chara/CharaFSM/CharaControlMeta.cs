


using System;
using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 角色控制的元数据信息
    /// </summary>
    public struct CharaControlMeta
    {
        public Func<Vector3> up;
        public Func<Vector3> right;
        public Func<Vector3> forward;
        public float height;
        public float radius;
        public float slopLimit;
        public float stepOffset;
        public float runSpeed;
        public float walkSpeed;
        public float jumpSpeed;
        public float raiseGrivity;
        public float fallGrivity;
    }
}