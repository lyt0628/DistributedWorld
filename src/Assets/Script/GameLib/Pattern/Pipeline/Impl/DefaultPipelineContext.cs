


namespace GameLib.Pattern
{
    public class DefaultPipelineConext : IPipelineContext
    {
        private readonly DefaultPipeline pipeline = new DefaultPipeline();
        public IPipeline Pipeline => pipeline;

        public object InBound(object msg)
        {
            pipeline.root.InBound = true;
            pipeline.root.Next(msg);
            return msg;
        }

        public object OutBound(object msg)
        {
            pipeline.tail.InBound = false;
            pipeline.tail.Next(msg);
            return msg;
        }
    }
}