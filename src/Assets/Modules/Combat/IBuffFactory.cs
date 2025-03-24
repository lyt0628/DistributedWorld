

using QS.Combat;

namespace QS.Api.Combat.Service
{
    public interface IBuffFactory
    {
        IBuff LinearAP(float apSlop, float apIntercept);
        IBuff LinearMP(float mpSlop, float mpIntercept);
        IBuff LinearDef(float atk, float matk);
        IBuff LinearMdef(float mdefSlop, float mdefIntercept);
    }
}