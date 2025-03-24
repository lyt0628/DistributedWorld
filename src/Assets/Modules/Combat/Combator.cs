


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Combat
{
    /// <summary>
    /// 战斗者的数据实体
    /// </summary>
    class Combator : ICombator
    {
        public UnityEvent OnDataChanged { get; } = new();
        readonly HashSet<IBuff> buffs = new();

        /// <summary>
        /// 给定数据的基本构造器
        /// </summary>

        public Combator(float maxHP, float maxMana, float maxStamina, float attackPower, float magicPower, float defence, float magicDefence)
        {

            MaxHP = maxHP;
            Hp = maxHP;
            MaxMana = maxMana;
            Mana = maxMana;
            MaxStamina = maxStamina;
            AttackPower = attackPower;
            MagicPower = magicPower;
            Defence = defence;
            MagicDefence = magicDefence;

        }

        /// <summary>
        /// 给定数据和初始 Buff
        /// </summary>
        public Combator(HashSet<IBuff> buffs, float maxHP, float maxMana, float maxStamina, float attackPower, float magicPower, float defence, float magicDefence)
        {
            this.buffs = buffs ?? throw new ArgumentNullException(nameof(buffs));
            MaxHP = maxHP;
            MaxMana = maxMana;
            MaxStamina = maxStamina;
            AttackPower = attackPower;
            MagicPower = magicPower;
            Defence = defence;
            MagicDefence = magicDefence;
        }

        public Combator(float hp, float maxHP, float mana, float maxMana, float stamina, float maxStamina, float attackPower, float magicPower, float defence, float magicDefence)
        {
            Hp = hp;
            MaxHP = maxHP;
            Mana = mana;
            MaxMana = maxMana;
            Stamina = stamina;
            MaxStamina = maxStamina;
            AttackPower = attackPower;
            MagicPower = magicPower;
            Defence = defence;
            MagicDefence = magicDefence;
        }

        public float Hp { get; set; }

        public float MaxHP { get; set; }

        public float Mana { get; set; }

        public float MaxMana { get; set; }

        public float Stamina { get; set; }

        public float MaxStamina { get; set; }

        public float AttackPower { get; set; }

        public float BuffedAttackPower
        {
            get
            {
                var ap = AttackPower;
                float acc = ap;
                foreach (var buff in buffs)
                {
                    acc = buff.AttackPowerPlus(ap, acc);
                }
                return acc;
            }
        }

        public float MagicPower { get; set; }

        public float BuffedMagicPower
        {
            get
            {
                var mp = MagicPower;
                float acc = mp;
                foreach (var buff in buffs)
                {
                    acc = buff.AttackPowerPlus(mp, acc);
                }
                return acc;
            }
        }

        public float Defence { get; set; }

        public float BuffedDefence
        {
            get
            {
                var def = Defence;
                float acc = def;
                foreach (var buff in buffs)
                {
                    acc = buff.AttackPowerPlus(def, acc);
                }
                return acc;
            }
        }

        public float MagicDefence { get; set; }

        public float BuffedMagicDefence
        {
            get
            {
                var mdef = MagicDefence;
                float acc = mdef;
                foreach (var buff in buffs)
                {
                    acc = buff.AttackPowerPlus(mdef, acc);
                }
                return acc;
            }
        }

        public ITalent talent { get; set; }

        public void AddBuff(IBuff buff)
        {
            if (!buffs.Add(buff))
            {
                Debug.Log("Buff already added");
            }
        }

        public void RemoveBuff(IBuff buff)
        {
            if (!buffs.Remove(buff))
            {
                Debug.LogWarning($"Buff buff does not contained in Combator");
            }
        }
    }
}