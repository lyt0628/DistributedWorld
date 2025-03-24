namespace QS.GameLib.Pattern.Pipeline
{
    public interface IPipelineHandler
    {
        void Read(IPipelineHandlerContext context, object msg);
        void Write(IPipelineHandlerContext context, object msg);
    }
}