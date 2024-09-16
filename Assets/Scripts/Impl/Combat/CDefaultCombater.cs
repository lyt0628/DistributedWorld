using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using QS.API;

namespace QS
{
    class CDefaultCombater :  AbstractCombater
    {

        public void Awake() 
        {
            var data = new CCombatData
            {
                Hp = 100,
                Mp = 100,
                Atn = 100,
                Def = 100,
                Matk = 50,
                Res = 100
            };

            CombatData = data;
            Combating = false;
            Enemies = new();

            var buffData = new CBuffData()
            {
                Atn = 10,
                Def = 10,
                Res = 10,
                Matk = 10,
            };
            var buf = new CABuff(buffData);
            AcceptBuff(buf);
        }

        public void Update()
        {
            var att = Attack();
            Debug.Log(string.Format("111Atk: {0}, Matk : {1}", att.Atn, att.Matk));
        }
    }
}