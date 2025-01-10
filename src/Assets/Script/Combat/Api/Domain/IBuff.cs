namespace QS.Api.Combat.Domain
{

    public interface IBuff
    {
        public BuffStage Stage { get; }

        public string Id { get; }

    }
}
