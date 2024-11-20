using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor.TestTools;
using UnityEngine.TestTools;




namespace GameLib.DI.Test.GenericInectionTest
{
    class A<T>{}
    class AInt : A<int>
    {
    }
    class B {
        [Injected]
        public A<int> aint;
    }


    class GenericInjectionTest
    {
        [Test]
        public void Gneric_Injection_Works()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(AInt))
                .Bind(typeof(B));
            var b = ctx.GetInstance<B>(typeof(B));
            Assert.IsNotNull(b);
            Assert.IsNotNull(b.aint);
        }
    }


}