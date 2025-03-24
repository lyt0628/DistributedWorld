using GameLib.DI;
using NUnit.Framework;

public class HierarchyTest
{
    class A
    {
    }

    class B
    {
        [Injected]
        public A a;
    }

    [Test]
    public void HierarchyTestSimplePasses()
    {
        var ctx1 = IDIContext.New();
        var ctx2 = IDIContext.New();
        ctx1.Bind<A>();
        ctx2.Bind<B>();
        ctx2.SetParent(ctx1);
        var b = ctx2.GetInstance<B>();
        Assert.IsNotNull(b);
        Assert.IsNotNull(b.a);
    }

}
