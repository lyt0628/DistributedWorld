

namespace GameLib.Pattern
{
    public interface IBuildDirector<T>
    {
        public void SetBuilder(IBuilder<T> builder);
        public T  Build();
    }
}