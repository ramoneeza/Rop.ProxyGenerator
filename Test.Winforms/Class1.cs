using Rop.ProxyGenerator.Annotations;
using Rop.Winforms.Basic.Interfaces;
using System;
using System.Windows.Forms;

namespace Test.Winforms
{
    internal partial class dummy { }

    [ProxyOf("IControlValue<int>", nameof(_controlValueProxy))]
    public partial class TextBoxValue : TextBox, IControlValue<int>
    {
        private readonly IControlValue<int> _controlValueProxy;
        
    #region IControlValue
        public virtual int ControlValue
        {
            get => int.TryParse(base.Text, out int v) ? v : -1;
            set => base.Text = value.ToString();
        }
        protected override void OnTextChanged(EventArgs eventargs)
        {
            base.OnTextChanged(eventargs);
            OnControlValueChanged();
        }
        public override string Text
        {
            get => ControlValue.ToString();
            set
            {
                if (int.TryParse(value, out var i)) base.Text = i.ToString();
            }
        }

        private class TextBoxValueAop : ControlValueAop<TextBoxValue, int>
        {
            public TextBoxValueAop(TextBoxValue parent) : base(parent)
            {
            }
        }
    #endregion

        public TextBoxValue()
        {
            _controlValueProxy = new TextBoxValueAop(this);
        }

    }
}