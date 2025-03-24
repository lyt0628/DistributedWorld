namespace QS.GameLib.Pattern.Pipeline
{
    public class DefaultPipelineConext : IPipelineContext
    {
        private readonly DefaultPipeline pipeline = new();

        public IPipeline Pipeline => pipeline;

        public object InBound(object msg)
        {
            if (pipeline.root == null) return msg;

            pipeline.root.InBound = true;
            pipeline.root.Next(msg);
            return msg;
        }

        public object OutBound(object msg)
        {
            if (pipeline.tail == null) return msg;
            pipeline.tail.InBound = false;
            pipeline.tail.Next(msg);
            return msg;
        }
    }
}