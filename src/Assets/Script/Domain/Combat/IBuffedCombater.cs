
namespace QS.Domain.Combat
{

    public interface IBuffedCombater<T> : ICombater, IBuffable<T> where T : IBuff
    {
    }

}