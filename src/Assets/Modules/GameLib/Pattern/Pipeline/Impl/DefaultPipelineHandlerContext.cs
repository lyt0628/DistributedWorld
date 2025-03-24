namespace QS.GameLib.Pattern.Pipeline
{
    class DefaultPipelineHandlerContext : IPipelineHandlerContext
    {
        public DefaultPipelineHandlerContext(
            IPipeline pipeline, string handlerName, IPipelineHandler handler,
            DefaultPipelineHandlerContext preHander = null,
            DefaultPipelineHandlerContext nextHandler = null)
        {
            Pipeline = pipeline;
            HandlerName = handlerName;
            Handler = handler;
            PreHandlerContext = preHander;
            NextHandlerContext = nextHandler;
        }
        public IPipeline Pipeline { get; }
        public string HandlerName { get; }
        public IPipelineHandler Handler { get; }

        public bool InBound { get; set; } = true;

        public void Write(object msg)
        {
            var ctx = InBound ? NextHandlerContext : PreHandlerContext;
            if (ctx != null)
            {
                ctx.InBound = InBound;
                ctx.Next(msg);
            }

        }

        public void Next(object msg)
        {
            if (InBound)
            {
                Handler.Read(this, msg);
            }
            else
            {
                Handler.Write(this, msg);
            }
        }


        public DefaultPipelineHandlerContext PreHandlerContext { get; set; }
        public DefaultPipelineHandlerContext NextHandlerContext { get; set; }
    }

}