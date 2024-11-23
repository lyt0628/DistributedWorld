

namespace GameLib.Pattern
{
    public interface IPipelineHandlerContext
    {
        IPipeline Pipeline { get; }
        string HandlerName { get; }
        IPipelineHandler Handler { get; }

        bool InBound { get; set; }
        bool OutBound { get { return !InBound; } }
        void Write(object msg);
    }
}