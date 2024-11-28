


namespace QS.Domain.Combat
{
    class AttackMsg
    {
        public AttackMsg(float atn, float matk)
        {
            this.atn = atn;
            this.matk = matk;
        }
        public float atn;
        public float matk;
        public float atn_res;
        public float matk_res;
    }


    class InjureMsg
    {
        public InjureMsg(float def, float res)
        {
            this.def = def;
            this.res = res;
        }
        public float def;
        public float res;
        public float def_res;
        public float res_res;
    }

}