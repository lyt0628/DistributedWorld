using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using QS.GameLib.Rx.Relay;
using UnityEngine;
using UnityEngine.TestTools;

public class RelayMapOperatorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Just_Int_To_String_With_1_Element()
    {

        Relay<int>
            .Just(new List<int>() { 1 })
            .Map((i) => i.ToString())
            .Subscrib((s) =>Assert.AreEqual("1", s));
    }

}
