using UnityEngine.UIElements;


public partial class LButton : VisualElement
{
    /// <summary>
    /// 控件的屬性得在 元素類裏面定義，
    /// 這裏是 Model的地方
    /// 下面的內部類則是 與Unity交互，註冊的地方
    /// </summary>
    public string myString { get; set; }
    public int myInt { get; set; }

    /// <summary>
    /// 固定格式，告訴Unity自己定義了 新控件
    /// </summary>
    public new class UxmlFactory : UxmlFactory<LButton, UxmlTraits> { }

    /// <summary>
    /// 設置控件屬性的地方
    /// </summary>
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        // 向來Unity 是通過反射來拿這邊的值的
        // 那麼定義了這個屬性就是註冊了
        UxmlStringAttributeDescription m_String = new()
        { name = "my-string", defaultValue = "default_value" };
        UxmlIntAttributeDescription m_Int = new()
        { name = "my-int", defaultValue = 2 };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as LButton;

            /// 從 UI 中獲取 屬性值 V => M
            ate.myString = m_String.GetValueFromBag(bag, cc);
            ate.myInt = m_Int.GetValueFromBag(bag, cc);
        }


    }
}
