

namespace QS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;   
    using UnityEngine;
    
    using QS.API;
    public abstract class AbstractCombater : MonoBehaviour, IBuffedCombater
    {
        public ICombatData CombatData { get; set; }
        public bool Combating {get; set;}
        public List<ICombatable> Enemies { get; set; }


        private Func<ICombatData, ICombatData> BeforeAttack;
        private Func<IAttack, IAttack> AfterAttack;
        private Func<ICombatData, ICombatData> BeforeInjured;
        private Func<ICombatData, ICombatData> AfterInjured;

        public AbstractCombater()
        { 
            Combating = false;
            Enemies = new();

            BeforeAttack += (x) => x;
            AfterAttack += (x) => x;
            BeforeInjured += (x) => x;
            AfterInjured += (x) => x;


        }

        public IAttack Attack()
        {
            var combatData = BeforeAttack.Invoke(CombatData);

            //Debug.Log(string.Format("999: {0}, Matk : {1}", combatData.Atn, combatData.Matk));
            var att = CAttack.FromCombatData(combatData);
            //Debug.Log(string.Format("222Atk: {0}, Matk : {1}", att.Atn, att.Matk));
            att = AfterAttack.Invoke(att);

            //Debug.Log(string.Format("333Atk: {0}, Matk : {1}", att.Atn, att.Matk));
            return att;
        }

        public void Injured(IAttack attack)
        {
            var combatData = BeforeInjured.Invoke(CombatData);
            combatData = CAttack.ComputeCombatData(combatData, attack);
            CombatData = AfterInjured.Invoke(combatData);

            Debug.LogFormat("Hp Of {0} is {1}", name, CombatData.Hp);
        }

        public void AcceptBuff(IBuff buff)
        {
            Debug.Log(buff.GetAttackStage().ToString());
            switch (buff.GetAttackStage())
            {
                case EBuffStage.IMMEDIATE: 
                    CombatData = buff.BuffOnCombatData(CombatData);
                    break;
                case EBuffStage.BEFORE_ATTACK: 
                    BeforeAttack += buff.BuffOnCombatData;
                    break;
                case EBuffStage.AFTER_ATTACK: 
                    AfterAttack += buff.BuffOnAttack;
                    break;
                case EBuffStage.BEFORE_INJURED: 
                    BeforeInjured += buff.BuffOnCombatData;
                    break;
                case EBuffStage.AFTER_INJURED: 
                    AfterInjured += buff.BuffOnCombatData;
                    break;
                default: 
                    throw new ArgumentException(string.Format("Unknown Attack Stage {0}", buff.GetAttackStage())); 
            }
        }

    }
}
