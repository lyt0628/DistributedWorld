

//using GameLib.DI;
//using QS.Api;
//using QS.Api.Common.Util.HitDetector;
//using QS.Chara.Domain;
//using QS.Executor;
//using QS.Skill.Domain.Handler;
//using System;
//using System.Linq;
//using UnityEngine;


///// <summary>
///// Action的部分在动画规范设计好之前没法继续进展了(我对动画-动作没概念)
///// 先做RPG部分, 问题是要讲什么风格的故事, 战斗流程是怎么样的, 游戏节奏是什么样的
///// 主线是 - 打Boss(GameAI)
///// 要给等级的概念吗?
///// 职业要有
///// 动作设计 - 有什么动作
///// 剧情动画(Skip)
///// 
///// </summary>
//class SabreAttack : SimpleSkillSubHandlerAdapter
//{
//    [Injected]
//    IDetectorFactory detectorFactory;

//    [Injected]
//    ILifecycleProivder life;

//    ISpanDetector detector;
//    GameObject sphere;

//    void DoDetection()
//    {
//            if (!detector.Enabled) return;

//            var objs = detector.Detect();
//            if (objs.Any())
//            {
//               GameObject.Destroy(sphere);
//                detector.Disable();
//               var obj = objs.First();
//                var e = obj.GetComponent<ExecutorBehaviour>();
//            if (e != null)
//            {
//                Debug.Log(e);
//                GameObject.Destroy(obj);
//            }

//            }

//    }

//    public override void OnCastingEnter(Character chara)
//    {
//        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//        var g = sphere.AddComponent<Rigidbody>();
//        var c = sphere.GetComponent<Collider>();
//        sphere.transform.position = chara.transform.position;
//        sphere.transform.position += new Vector3(0, 1.3f, 0) + chara.transform.forward * 3;
//        sphere.transform.localScale = new Vector3(2, 2, 2);
//        g.useGravity = false;
//        g.velocity = chara.transform.forward * 10;
//        detector = detectorFactory.Collide(c, CollideStage.Enter);
//        detector.Enable();

//        life.Request(Lifecycles.Update, DoDetection);
//    }

//    public override void OnCastingExit(Character chara)
//    {
//        life.Cancel(Lifecycles.Update, DoDetection);
//        detector?.Disable();

//    }
//}