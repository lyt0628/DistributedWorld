using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QS.API;
using QS.API.Service.Domain;


namespace QS.Impl.Service.Domain
{
    public class CBuffGroup : AbstractBuff
    {
        private readonly ICollection<IBuff> buffs;

        public CBuffGroup(ICollection<IBuff> buffs) : base(null)
        {
            this.buffs = buffs;
        }

        override public BuffStages AttackStage => BuffStages.None;

        public override void DoBuff(IBuffable buffable)
        {
            foreach (IBuff buff in buffs)
            {
                buff.DoBuff(buffable);
            }
        }
    }
}