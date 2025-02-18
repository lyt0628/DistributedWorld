


namespace QS.Api.Executor.Domain
{
    public interface IInstructionPipeline 
    {
        void AddFirst(string name, IInstructionHandler handler);
        void AddLast(string name, IInstructionHandler handler);
        void AddBefore(string baseName, string name, IInstructionHandler handler);
        void AddAfter(string baseName, string name, IInstructionHandler handler);
        void Remove(string name);
        void Remove(IInstructionHandler handler);
        T Get<T>() where T : IInstructionHandler;
    }
}