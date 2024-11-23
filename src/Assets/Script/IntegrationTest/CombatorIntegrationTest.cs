using GameLib.DI;
using GameLib.Pattern;
using NUnit.Framework;

public class CombatorIntegrationTest
{
    abstract class PipelineHolder
    {
        [Injected]
        IPipelineContext ctx1;
        [Injected]
        IPipelineContext ctx2;

        public void check1()
        {
            Assert.IsNotNull(ctx1);
        }
        public void check2()
        {
            Assert.IsNotNull(ctx2);
        }
    }

    class PipelineHolderChild : PipelineHolder
    {
    }
    class Another
    {
        [Injected]
        public PipelineHolder child;
    }
    // A Test behaves as an ordinary method
    [Test]
    public void CombatorIntegrationTestSimplePasses()
    {
        var ctx = IDIContext.New();
        ctx.Bind(typeof(DefaultPipelineConext))
            .Bind(typeof(PipelineHolderChild))
            .Bind(typeof(Another));
        var holder = ctx.GetInstance<Another>(typeof(Another)).child;
        Assert.IsNotNull(holder);
        holder.check1();
        holder.check2();
    }

}
