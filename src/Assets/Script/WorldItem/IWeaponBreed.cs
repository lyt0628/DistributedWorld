using QS.Api.Combat.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The weapon Defferent fields with item.
    /// and used as a flyWeight definition 
    /// 
    /// �����ǲ�ͬ����ʵ��Ҳ����ͬ�����������ҪBreed�����û�еĻ�������
    /// No��������������ӣ�����UI�����ǹ�ͨ�ģ�
    /// ������������Ҫ����UI����ҲӦ���Ƕ��������ģ����
    /// ����˵һ��ItemRankBreed �İ�װ��
    /// </summary>
    public interface IWeaponBreed : IItemBreed
    {
        IBuff MainBuff { get; }
    }
}