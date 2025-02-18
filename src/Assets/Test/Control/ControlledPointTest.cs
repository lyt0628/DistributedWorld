//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;

//using QS.Control;
//using GameLib.DI;
//using QS.GameLib.Rx.Relay;


//public class ControlledPointTest
//{
//    [Test]
//    public void Binding_For_Controlled_Point_Exists()
//    {
//        var di =  IDIContext.New();
//        ControlGlobal.Instance.ProvideBinding(di);

//        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
//        var control = di.GetInstance<ICharaTranslationControl>();
//        Assert.IsNotNull(ds);
//        Assert.IsNotNull(control);

//    }


//    // A Test behaves as an ordinary method
//    [Test]
//    public void DO_Create_Method_Reurn_A_Value_Object()
//    {
//        var di =  IDIContext.New();
//        ControlGlobal.Instance.ProvideBinding(di);

//        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
//        var data = ds.Create();
//        Assert.IsNotNull(data);

//    }
//    [Test]
//    public void DO_Relay_Cycle_Works()
//    {
//        var di =  IDIContext.New();
//        ControlGlobal.Instance.ProvideBinding(di);

//        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
//        var control = di.GetInstance<ICharaTranslationControl>();

//        var data = ds.Create();
//        data.vertical = 0f;
//        data.horizontal = 0f;
//        data.jump = false;
//        data.baseRight = Vector3.right;
//        data.baseForword = Vector3.forward;
//        data.baseUp = Vector3.up;
        

//        var dataRelay = Relay<ICharaTranslationSnapshot>
//            .Tick(() => data, out IMotion motion);

//        var uuid = ds.New(dataRelay);
//        Assert.IsNotNull (uuid);

//        var tRelay = control.GetTranslation(uuid);
//        Assert.IsNotNull (tRelay);

//        tRelay.Subscrib(t => Assert.IsNotNull(t));

//        motion.Set();
//    }
    
//}
