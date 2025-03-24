using UnityEngine;
using UnityEngine.UIElements;


namespace QSUILibrary
{
    public class FillSlider : BaseField<float>
    {
        public new class UxmlFactory : UxmlFactory<FillSlider, UxmlTraits> { }
        public new class UxmlTraits : BaseFieldTraits<float, UxmlFloatAttributeDescription> { }

        public static readonly new string ussClassName = "qs-fill-slider";
        public static readonly new string inputUssClassName = "qs-fill-slider__input";
        public static readonly string fillAreaUssClassName = "qs-fill-slider__fill-area";


        VisualElement mInputEl;
        VisualElement mFillArea;

        public FillSlider() : this(null)
        {
        }
        public FillSlider(string label) : base(label, null)
        {

            AddToClassList(ussClassName);

            mInputEl = this.Q(className: BaseField<float>.inputUssClassName);
            mInputEl.AddToClassList(inputUssClassName);
            Add(mInputEl);

            mFillArea = new();
            mFillArea.AddToClassList(fillAreaUssClassName);
            Add(mFillArea);

        }

        public override void SetValueWithoutNotify(float newValue)
        {
            base.SetValueWithoutNotify(newValue);

            mFillArea.style.width = new Length(Mathf.Clamp(newValue, 0, 1) * 100, LengthUnit.Percent);
        }
    }
}