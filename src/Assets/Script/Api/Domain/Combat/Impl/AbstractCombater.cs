using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using System;
using System.Collections.Generic;


namespace QS.Domain.Combat
{

    /// <summary>
    /// ���չ��� = ��ɫ����*Buf 
    /// ���շ��� = ��ɫ��������*Buff 
    /// �˺�=ϵ��*����^2/ϵ��*(ϵ��*����+����) 
    /// </summary>
    public abstract class AbstractCombater
        : IBuffedCombater<AbstractBuff>, IMessagerProvider
    {

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
        public bool Combating { get; set; }
        public List<ICombatable> Enemies { get; set; }

        private readonly IMessager _messager = new Messager();
        public IMessager Messager
        {
            get
            {
                return _messager;
            }
        }

        public virtual IAttack Attack()
        {
            var atk = new AttackMsg(CombatData.Atn, CombatData.Matk);
            attackPipelineContext.InBound(atk);

            var result = new Attack
            {
                Atn = atk.atn_res,
                Matk = atk.matk_res
            };
            return result;
        }

        public virtual void Injured(IAttack atk)
        {
            var def = new InjureMsg(CombatData.Def, CombatData.Res);
            injuredPipelineContext.InBound(def);

            CombatData.Hp = CombatData.Hp - atk.Atn * atk.Atn / (atk.Atn + def.def_res);
            Messager.Boardcast("HP", new Msg1<float>(CombatData.Hp));
        }

        public virtual void AddBuff(string id, AbstractBuff buff)
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
