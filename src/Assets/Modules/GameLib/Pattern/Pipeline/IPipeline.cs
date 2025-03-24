namespace QS.GameLib.Pattern.Pipeline
{
    public interface IPipeline
    {
        void AddFirst(string name, IPipelineHandler handler);
        void AddLast(string name, IPipelineHandler handler);
        void AddBefore(string baseName, string name, IPipelineHandler handler);
        void AddAfter(string baseName, string name, IPipelineHandler handler);
        void Remove(string name);
        void Remove(IPipelineHandler handler);
        T Get<T>() where T : IPipelineHandler;

        //TODO: RemoveByType
        //public void Remove<T>(T handler) where T : IPipelineHandler;

    }

}