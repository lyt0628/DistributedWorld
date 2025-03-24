using GameLib;

namespace QS.Domain.Item
{
    /// <summary>
    /// IItem is a thing that can be stored in Inventory.
    /// </summary>
    public interface IItem : IClonable<IItem>
    {
        /// <summary>
        /// ���������, �����������ΨһID
        /// </summary>
        string Name { get; }
        /// <summary>
        /// �����uuid, ÿ������ʵ�������Լ���uuid, 
        /// Ψһ��ʶ�Լ��������.
        /// ����, ԭ�������uuid����һ����, ֻ�е�������岻��
        /// ��ԭ�ͲŻ���Ϊ�����ɶ�����uuid
        /// 
        /// ����ṩ���ԭ������, ��Ӧ����һ������, ��Ӧ��ע��,
        /// ʹ�� һ���������ṩ���ԭ��
        /// 
        /// �������� ��������Ҫ��ô�洢, ����Ӧ�ø��и��Ĵ洢, �����(��Ϊ�ǻ��¼, ��������)
        /// </summary>
        string UUID { get; set; }
        ItemType Type { get; }

        string Img { get; }
        string Prefab { get; }
        string Description { get; }


    }
}
