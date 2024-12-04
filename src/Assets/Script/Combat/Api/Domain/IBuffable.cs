namespace QS.Api.Combat.Domain
{
    public interface IBuffable<in T> where T : IBuff
    {
        public void AddBuff(string id, T buff);

        public void RemoveBuff(string id, BuffStages stage);

    }


}