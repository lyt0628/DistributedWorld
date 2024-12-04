namespace QS.Api.Combat.Domain
{

    public interface IBuffedCombater<T> : ICombater, IBuffable<T> where T : IBuff
    {
    }

}