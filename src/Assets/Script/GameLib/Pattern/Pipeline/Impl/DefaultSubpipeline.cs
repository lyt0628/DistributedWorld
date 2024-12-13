


namespace QS.GameLib.Pattern.Pipeline
{
    class DefaultSubpipeline : ISubpipeline
    {
        readonly IPipelineContext subpipelineContext = IPipelineContext.New();

        public void AddAfter(string baseName, string name, IPipelineHandler handler)
        {
           subpipelineContext.Pipeline.AddAfter(baseName, name, handler);
        }

        public void AddBefore(string baseName, string name, IPipelineHandler handler)
        {
            subpipelineContext.Pipeline.AddBefore(baseName, name, handler);
        }

        public void AddFirst(string name, IPipelineHandler handler)
        {
            subpipelineContext.Pipeline.AddFirst(name, handler);
        }

        public void AddLast(string name, IPipelineHandler handler)
        {
            subpipelineContext.Pipeline.AddLast(name, handler);
        }


        public void Remove(string name)
        {
            subpipelineContext.Pipeline.Remove(name);
        }

        public void Remove(IPipelineHandler handler)
        {
            subpipelineContext.Pipeline.Remove(handler);
        }
        public void Read(IPipelineHandlerContext context, object msg)
        {
            var trunk = new PipelineTunkHandler(context);
            subpipelineContext.Pipeline.AddLast("Subpipeline TrunkHandler", trunk);
            subpipelineContext.InBound(msg);
            subpipelineContext.Pipeline.Remove(trunk);
        }

        public void Write(IPipelineHandlerContext context, object msg)
        {
            var trunk = new PipelineTunkHandler(context);
            subpipelineContext.Pipeline.AddFirst("Subpipeline TrunkHandler", trunk);
            subpipelineContext.OutBound(msg);
            subpipelineContext.Pipeline.Remove(trunk);
        }

        class PipelineTunkHandler : IPipelineHandler
        {
            readonly IPipelineHandlerContext superPipelineNextHandlerContext;

            public PipelineTunkHandler(IPipelineHandlerContext superPipelineNextHandlerContext) 
            {
                this.superPipelineNextHandlerContext = superPipelineNextHandlerContext;
            }

            public void Read(IPipelineHandlerContext context, object msg)
            {
                superPipelineNextHandlerContext.Write(msg);
            }

            public void Write(IPipelineHandlerContext context, object msg)
            {
                superPipelineNextHandlerContext.Write(msg);
            }
        }
    }
}