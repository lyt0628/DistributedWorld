using UnityEngine.UIElements;


public partial class LButton : VisualElement
{
    /// <summary>
    /// �ؼ��Č��Ե��� Ԫ����Y�涨�x��
    /// �@�Y�� Model�ĵط�
    /// ����ăȲ�t�� �cUnity�������]�Եĵط�
    /// </summary>
    public string myString { get; set; }
    public int myInt { get; set; }

    /// <summary>
    /// �̶���ʽ�����VUnity�Լ����x�� �¿ؼ�
    /// </summary>
    public new class UxmlFactory : UxmlFactory<LButton, UxmlTraits> { }

    /// <summary>
    /// �O�ÿؼ����Եĵط�
    /// </summary>
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        // ���Unity ��ͨ�^��������@߅��ֵ��
        // ���N���x���@�����Ծ����]����
        UxmlStringAttributeDescription m_String = new()
        { name = "my-string", defaultValue = "default_value" };
        UxmlIntAttributeDescription m_Int = new()
        { name = "my-int", defaultValue = 2 };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as LButton;

            /// �� UI �Ы@ȡ ����ֵ V => M
            ate.myString = m_String.GetValueFromBag(bag, cc);
            ate.myInt = m_Int.GetValueFromBag(bag, cc);
        }


    }
}
