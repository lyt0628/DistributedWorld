using GameLib.DI;
using QS.Api.Data;
using QS.Domain.Combat;
using QS.GameLib.Pattern.Message;
using System.Collections.Generic;
using UnityEngine;

public class CombatorBehaviour : MonoBehaviour, IBuffedCombater<AbstractBuff>, IMessagerProvider
{
    [Injected]
    readonly IPlayerCharacterData playerCharacter;

    [Injected]
    readonly AbstractCombater delegat;

    public bool Combating { get => ((ICombater)delegat).Combating; set => ((ICombater)delegat).Combating = value; }
    public List<ICombatable> Enemies { get => ((ICombater)delegat).Enemies; set => ((ICombater)delegat).Enemies = value; }
    public ICombatData CombatData { get => ((ICombatable)delegat).CombatData; set => ((ICombatable)delegat).CombatData = value; }

    public IMessager Messager => ((IMessagerProvider)delegat).Messager;

    public void AddBuff(string id, AbstractBuff buff)
    {
        ((IBuffable<AbstractBuff>)delegat).AddBuff(id, buff);
    }

    public IAttack Attack()
    {
        return ((IAttackable)delegat).Attack();
    }

    public void Injured(IAttack attack)
    {
        ((IInjurable)delegat).Injured(attack);
    }

    public void RemoveBuff(string id, BuffStages stage)
    {
        ((IBuffable<AbstractBuff>)delegat).RemoveBuff(id, stage);
    }

    void Start()
    {
        var ctx = GameManager.Instance.GlobalDIContext;
        ctx.Inject(this);

        playerCharacter.ActivedCharacter = gameObject;

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
