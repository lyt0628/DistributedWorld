using GameLib.DI;
using Newtonsoft.Json;
using QS.Api.Combat.Domain;
using QS.Chara.Domain;
using QS.Combat.Domain;
using QS.Executor;
using QS.GameLib.Pattern.Message;
using QS.PlayerControl;
using UnityEngine;

public class CombatorBehaviour 
    : MonoBehaviour, IBuffedCombater, IMessagerProvider
{

    [Injected]
    readonly AbstractCombater delegat;

    //[Injected]
    //readonly IPlayerCharacterData playerCharacter;

    public ICombatData CombatData { get => ((ICombater)delegat).CombatData;  }

    public IMessager Messager => ((IMessagerProvider)delegat).Messager;

    public ICombatData MaxCombatData => ((ICombater)delegat).MaxCombatData;

    public void AddBuff<T>(string id, AbstractBuff<T> buff)
    {
        ((IBuffable)delegat).AddBuff(id, buff);
    }

    public IAttack Attack()
    {
        return ((IAttackable)delegat).Attack();
    }

    public void Injured(IAttack attack)
    {
        ((IInjurable)delegat).Injured(attack);
    }

    public void RemoveBuff(string id, BuffTarget stage)
    {
        ((IBuffable)delegat).RemoveBuff(id, stage);
    }


    void Start()
    {
        TrunkGlobal.Instance.DI.Inject(this);
        delegat.CombatData = new CombatData()
        {
            Atn = 0,
            Matk = 0,
            Def = 0,
            Res = 0,
            Hp = 0,
            Mp = 0
        };

        //playerCharacter.ActivedCharacter = gameObject.GetComponent<Character>();
        delegat.CombatData = new CombatData()
        {
            Atn = 100,
            Matk = 100,
            Def = 100,
            Res = 100,
            Hp = 100,
            Mp = 100
        };

    }

}
