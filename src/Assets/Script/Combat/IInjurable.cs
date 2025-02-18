namespace QS.Api.Combat.Domain
{

    public interface IInjurable
    {
        public void Injured(IAttack attack);
    }
}
