using QS.Api.Combat.Domain;
using QS.Combat.Service;
using QS.Common;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using System;
using System.Collections.Generic;


namespace QS.Combat.Domain
{

    /// <summary>
    /// 最终攻击 = 角色属性*Buf 
    /// 最终防御 = 角色防御属性*Buff 
    /// 伤害=系数*攻击^2/系数*(系数*攻击+防御) 
    /// </summary>
    public abstract class AbstractCombater
        : IBuffedCombater, IMessagerProvider
    {
        public AbstractCombater(ICombatData maxCombatData) 
        {
            MaxCombatData = new CombatData()
            {
                Atn = 100,
                Matk = 100,
                Def = 100,
                Res = 100,
                Hp = 100,
                Mp = 100
            };
            CombatData = MaxCombatData;
            Messager = new Messager();
        }

        readonly IPipelineContext combatDataBuffs = IPipelineContext.New();

        readonly IPipelineContext attackPipelineContext = IPipelineContext.New();

        readonly IPipelineContext injuredPipelineContext = IPipelineContext.New();
        

        public ICombatData MaxCombatData { get; }
        public ICombatData CombatData { get; set; }

        public IMessager Messager{ get; }

        public virtual IAttack Attack()
        {
            var atk = new AttackMsg(CombatData.Atn, CombatData.Matk);
            attackPipelineContext.InBound(atk);

            var f = new AttackFactory();
            return f.NewAttack(atk.atn_res, atk.matk_res);
        }

        public virtual void Injured(IAttack atk)
        {
            var def = new InjureMsg(CombatData.Def, CombatData.Res);
            injuredPipelineContext.InBound(def);

            var hp = CombatData.Hp - atk.Atn * atk.Atn / (atk.Atn + def.def_res);
            CombatData = new CombatData()
            {
                Hp = hp,
                Mp = CombatData.Mp,
                Atn = CombatData.Atn,
                Matk = CombatData.Matk,
                Def = CombatData.Def, 
                Res = CombatData.Res,
            };

        }

        public virtual void AddBuff<T>(string id, AbstractBuff<T> buff) 
        {
            switch (buff.Stage)
            {
                case BuffTarget.Attack:
                    attackPipelineContext.Pipeline.AddLast(id, buff);
                    break;
                case BuffTarget.Injure:
                    injuredPipelineContext.Pipeline.AddLast(id, buff);
                    break;
                case BuffTarget.None:
                    break;
                default: throw new InvalidOperationException();
            }
        }

        public void RemoveBuff(string id, BuffTarget stage)
        {
            switch (stage)
            {
                case BuffTarget.Attack:
                    attackPipelineContext.Pipeline.Remove(id);
                    break;
                case BuffTarget.Injure:
                    injuredPipelineContext.Pipeline.Remove(id);
                    break;
            }
        }
    }
}
