using NUnit.Framework;



namespace GameLib.DI.Test
{
    class Rocket { }
    abstract class Parent
    {
        [Injected]
        public Rocket rocket;
    }

    class Child : Parent { }
    class HierachyInjectionTest
    {
        [Test]
        public void Parent_Injection_Works()
        {
            var ctx = IDIContext.New();
            ctx
                .Bind(typeof(Rocket))
                .Bind(typeof(Child));
            var c = ctx.GetInstance<Child>(typeof(Child));
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.rocket);
        }
    }
}
