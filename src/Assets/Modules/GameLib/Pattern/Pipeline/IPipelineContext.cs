
namespace QS.GameLib.Pattern.Pipeline
{
    public interface IPipelineContext
    {
        static IPipelineContext New()
        {
            return new DefaultPipelineConext();
        }
        IPipeline Pipeline { get; }

        object InBound(object msg);
        object OutBound(object msg);
    }
}