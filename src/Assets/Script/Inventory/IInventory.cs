namespace QS.Api.Inventory.Service
{

    /// <summary>
    /// ��֮���ؘ��@��inventoryģ�K��
    /// worlditem�ṩ�Ĺ����� �F��ֻ�� �������w�@�Nһ�����ܣ�
    /// ���еĘI�ղ��� ���� ���w�Լ������I����팍�F��
    /// 
    /// inventory ֻ��Ҫ���]�� ���л��������л����Ñ������ȹ��ܣ�
    /// �o�ɣ��@���� һ��repository
    /// 
    /// ��ʹ�����w���������I����팍�F������
    /// ǰ���O��ģ� ʹ�ßo�����õķ�ʽ������ǲ����ܵģ�
    /// �������͵����ֻ���x��һ��
    /// 
    ///
    /// </summary>
    public interface IInventory
    {
        public void AddItem(string itemName);
        public void RemoveItem(string itemUUID);

    }
}