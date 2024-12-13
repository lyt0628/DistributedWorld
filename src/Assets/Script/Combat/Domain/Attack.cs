using QS.Api.Combat.Domain;

namespace QS.Combat.Domain
{

    class Attack : IAttack
    {
        public Attack(float atk, float matk) 
        {
            Atn = atk;
            Matk = matk;
        }
        public float Atn { get;  }
        public float Matk { get;}

    }
}