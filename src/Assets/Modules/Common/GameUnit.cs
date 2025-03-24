namespace QS.Common
{
    /// <summary>
    /// ��Ϸ�������һ��ʵ�壬���ޣ��������
    /// </summary>
    public readonly struct GameUnit
    {
        public GameUnit(string id, UnitScope scope)
        {
            ID = id;
            Scope = scope;
        }

        /// <summary>
        /// ��λ��ΨһID��ȫ��Ψһ�򱾵�Ψһ
        /// �����ȫ��Ψһ���Ǿ������ݿ�ID������Զ�����ɵ�UUID������Ψһ���Ǳ������ɵ�UUID
        /// </summary>
        public string ID { get; }

        public UnitScope Scope { get; }
    }


    public enum UnitScope
    {
        /// <summary>
        /// ����Ψһ
        /// </summary>
        Local,

        /// <summary>
        /// ȫ��Ψһ
        /// </summary>
        Application
    }
}