namespace QS.Api.Combat.Domain
{

    public interface IBuff
    {
        public BuffStages AttackStage { get; }

        public string Id { get; }


    }
}
