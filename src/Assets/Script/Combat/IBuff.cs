namespace QS.Api.Combat.Domain
{

    public interface IBuff
    {
        public BuffTarget Stage { get; }

        public string Id { get; }

    }
}
