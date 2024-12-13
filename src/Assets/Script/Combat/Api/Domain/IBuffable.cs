using QS.Combat.Domain;

namespace QS.Api.Combat.Domain
{
    public interface IBuffable
    {
        public void AddBuff<T>(string id, AbstractBuff<T> buff);

        public void RemoveBuff(string id, BuffStages stage);

    }


}