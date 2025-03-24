


using QS.Api.Combat.Service;
using QS.Combat.Domain;

namespace QS.Combat.Service
{
    class BuffFactory : IBuffFactory
    {
        public IBuff LinearAP(float apSlop, float apIntercept)
        {
            return new LinearBuff(apSlop, apIntercept, 1, 0, 1, 0, 1, 0);
        }

        public IBuff LinearMP(float mpSlop, float mpIntercept)
        {
            return new LinearBuff(1, 0, mpSlop, mpIntercept, 1, 0, 1, 0);
        }

        public IBuff LinearDef(float defSlop, float defIntercept)
        {
            return new LinearBuff(1, 0, 1, 0, defSlop, defIntercept, 1, 0);
        }

        public IBuff LinearMdef(float mdefSlop, float mdefIntercept)
        {
            return new LinearBuff(1, 0, 1, 0, 1, 0, mdefSlop, mdefIntercept);
        }


    }
}