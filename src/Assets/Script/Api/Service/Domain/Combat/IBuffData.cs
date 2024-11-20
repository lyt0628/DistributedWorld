

using GameLib;

namespace QS.API
{
    public interface IBuffData : IClonable<IBuffData>
    {
    
        public float Hp {get;set;}
        public float Mp{get;set;}
        public float Atn{get;set;}
        public float Def {get;set;}
        public float Matk { get;set;}
        public float Res { get;set;}
    }
}

