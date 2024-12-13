namespace QS.GameLib.Pattern.Pipeline
{
    public interface IPipeline
    {
        public void AddFirst(string name, IPipelineHandler handler);
        public void AddLast(string name, IPipelineHandler handler);
        public void AddBefore(string baseName, string name, IPipelineHandler handler);
        public void AddAfter(string baseName, string name, IPipelineHandler handler);
        public void Remove(string name);
        public void Remove(IPipelineHandler handler);
        //TODO: RemoveByType
        //public void Remove<T>(T handler) where T : IPipelineHandler;

    }

}