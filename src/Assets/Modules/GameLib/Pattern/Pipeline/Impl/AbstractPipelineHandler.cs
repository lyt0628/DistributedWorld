namespace QS.GameLib.Pattern.Pipeline
{
    public abstract class AbstractPipelineHandler : IPipelineHandler
    {
        public abstract void Read(IPipelineHandlerContext context, object msg);
        public abstract void Write(IPipelineHandlerContext context, object msg);
    }
}