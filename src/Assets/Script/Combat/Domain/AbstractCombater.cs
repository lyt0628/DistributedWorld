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
        public AbstractCombater() 
        {
            Messager = CombatGlobal.Instance.GetInstance<IMessager>(Api.Common.DINames.GameLib_Message_Messager);
        }
        readonly IPipelineContext attackPipelineContext = IPipelineContext.New();

        readonly IPipelineContext injuredPipelineContext = IPipelineContext.New();

        protected ICombatData combatData;
        public ICombatData CombatData
        {
            get { return combatData; }
            set
            {
                combatData = value;
                Messager.Boardcast("HP", new Msg1<float>(combatData.Hp));
            }
        }

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
                Mp = combatData.Mp,
                Atn = combatData.Atn,
                Matk = combatData.Matk,
                Def = combatData.Def, 
                Res = combatData.Res,
            };

        }

        public virtual void AddBuff<T>(string id, AbstractBuff<T> buff) 
        {
            switch (buff.AttackStage)
            {
                case BuffStages.Attack:
                    attackPipelineContext.Pipeline.AddLast(id, buff);
                    break;
                case BuffStages.Injure:
                    injuredPipelineContext.Pipeline.AddLast(id, buff);
                    break;
                case BuffStages.None:
                    break;
                default: throw new InvalidOperationException();
            }
        }

        public void RemoveBuff(string id, BuffStages stage)
        {
            switch (stage)
            {
                case BuffStages.Attack:
                    attackPipelineContext.Pipeline.Remove(id);
                    break;
                case BuffStages.Injure:
                    injuredPipelineContext.Pipeline.Remove(id);
                    break;
            }
        }
    }
}
