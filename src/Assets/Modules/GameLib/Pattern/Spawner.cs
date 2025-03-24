namespace QS.GameLib.Pattern
{
    public interface ISpawner<T>
    {
        public T Spawn();
    }

    public class SpawnFor<T, U> : ISpawner<T> where U : T, new()
    {
        public T Spawn()
        {
            return new U();
        }

    }

}
