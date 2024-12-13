


namespace QS.Api.Executor.Domain
{
    public interface IInstructionPipeline 
    {
        public void AddFirst(string name, IInstructionHandler handler);
        public void AddLast(string name, IInstructionHandler handler);
        public void AddBefore(string baseName, string name, IInstructionHandler handler);
        public void AddAfter(string baseName, string name, IInstructionHandler handler);
        public void Remove(string name);
        public void Remove(IInstructionHandler handler);
    }
}