



namespace QS.GameLib.Pattern.Pipeline
{
    public interface ISubpipeline : IPipeline, IPipelineHandler
    {
        public static ISubpipeline New()
        {
            return new DefaultSubpipeline();
        }
    }
}