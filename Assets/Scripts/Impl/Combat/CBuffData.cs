


namespace QS
{
    using QS.API;
    class CBuffData : IBuffData
    {

        public CBuffData() { }
        public CBuffData(float hp, float mp, 
                            float atn, float def, 
                            float matk, float res ) { 
            Hp = hp;
            Mp = mp;
            Atn = atn;
            Def = def;
            Matk = matk;
            Res = res;
        }


        public float Hp {get;set;}
        public float Mp{get;set;}
        public float Atn{get;set;}
        public float Def {get;set;}
        public float Matk { get;set;}
        public float Res { get;set;}
        public IBuffData Clone()
        {
            return new CBuffData
            {
                Hp = Hp,
                Mp = Mp,
                Atn = Atn,
                Def = Def,
                Matk = Matk,
                Res = Res
            };
        }
    }
}