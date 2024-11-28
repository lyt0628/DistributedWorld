using NUnit.Framework;




namespace GameLib.DI.Test.GenericInectionTest
{
    class A<T> { }
    class AInt : A<int>
    {
    }
    class B
    {
        [Injected]
        public A<int> aint;
    }


    class GenericInjectionTest
    {
        [Test]
        public void Gneric_Injection_Works()
        {
            var ctx = IDIContext.New();
            ctx.Bind<AInt>()
                .Bind<B>();
            var b = ctx.GetInstance<B>();
            Assert.IsNotNull(b);
            Assert.IsNotNull(b.aint);
        }

    }


}