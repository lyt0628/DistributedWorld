using NUnit.Framework;

namespace GameLib.DI.Test
{
    public class InjectedInstanceTest
    {

        class A { }
        class B
        {
            [Injected]
            public A a;
        }


        [Test]
        public void Object_Can_be_Injected()
        {
            var ctx = IDIContext.New();
            var obj = new B();

            ctx.Bind(typeof(A))
                .Inject(obj);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.a);
        }
    }


}

