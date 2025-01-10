using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using QS.Control;
using GameLib.DI;
using QS.GameLib.Rx.Relay;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;

public class ControlledPointTest
{
    [Test]
    public void Binding_For_Controlled_Point_Exists()
    {
        var di =  IDIContext.New();
        ControlGlobal.Instance.ProvideBinding(di);

        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
        var control = di.GetInstance<ICharaTranslationControl>();
        Assert.IsNotNull(ds);
        Assert.IsNotNull(control);

    }


    // A Test behaves as an ordinary method
    [Test]
    public void DO_Create_Method_Reurn_A_Value_Object()
    {
        var di =  IDIContext.New();
        ControlGlobal.Instance.ProvideBinding(di);

        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
        var data = ds.Create();
        Assert.IsNotNull(data);

    }
    [Test]
    public void DO_Relay_Cycle_Works()
    {
        var di =  IDIContext.New();
        ControlGlobal.Instance.ProvideBinding(di);

        var ds = di.GetInstance<ICharaTranslationProxyDataSource>();
        var control = di.GetInstance<ICharaTranslationControl>();

        var data = ds.Create();
        data.Vertical = 0f;
        data.Horizontal = 0f;
        data.Jump = false;
        data.BaseRight = Vector3.right;
        data.BaseForword = Vector3.forward;
        data.BaseUp = Vector3.up;
        

        var dataRelay = Relay<ICharaTranslationSnapshot>
            .Tick(() => data, out IMotion motion);

        var uuid = ds.New(dataRelay);
        Assert.IsNotNull (uuid);

        var tRelay = control.GetTranslation(uuid);
        Assert.IsNotNull (tRelay);

        tRelay.Subscrib(t => Assert.IsNotNull(t));

        motion.Set();
    }
    
}
