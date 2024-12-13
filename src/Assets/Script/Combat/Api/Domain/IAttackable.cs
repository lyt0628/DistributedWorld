namespace QS.Api.Combat.Domain
{

    /// <summary>
    ///  获取基础攻击属性
    /// </summary>
    public interface IAttackable
    {
        public IAttack Attack();
    }
}