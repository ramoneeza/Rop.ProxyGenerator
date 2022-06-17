using Rop.ProxyGenerator.Annotations;
using Rop.Winforms.Basic.Interfaces;
using Rop.Winforms.Basic.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Test.Winforms
{
    internal partial class dummy { }
    [ProxyOf("IControlValue<DateTime?>", "_controlvalueproxy")]
    public partial class NullDatePickerValue : DateTimePicker, IControlValue<DateTime?>
    {
        protected static readonly DateTime _mindate = new DateTime(1901, 1, 1);
        #region IControlValue
        public virtual DateTime? ControlValue
        {
            get => this.Value >= MinDate ? Value : (DateTime?)null;
            set => this.Value = (value == null || value < MinDate) ? MinDate : value.Value;
        }
        protected bool LockEventChanges { get; set; }
        protected override void OnValueChanged(EventArgs eventargs)
        {
            if (LockEventChanges) return;
            base.OnValueChanged(eventargs);
            OnControlValueChanged();
        }
        public override string Text
        {
            get => IsNull ? "" : (ControlValue?.ToShortDateString() ?? "");
            set
            {
                if (DateTime.TryParse(value, out DateTime resultado)) ControlValue = resultado;
            }
        }

        public NullDatePickerValue()
        {
            base.MinDate = _mindate;
            IsNull = true;
            base.ValueChanged += Control_ValueChanged;
        }
        public bool IsNull
        {
            get => ControlValue == null;
            set => ControlValue = (value) ? null : ControlValue;
        }
        public new DateTime MinDate
        {
            get => (base.MinDate < _mindate) ? _mindate.AddDays(1) : base.MinDate.AddDays(1);
            set
            {
                if (value < _mindate) value = _mindate.AddDays(1);
                base.MinDate = value.AddDays(-1);
            }
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg())
            {
                case Wm.Paint:
                    if (IsNull) NullPaint();
                    else XPaint();
                    break;
                case Wm.Lbuttondown:
                    if (!IsNull) CheckDoNull(m.LParamToPoint());
                    break;
            }
        }

        private RectangleF GetXPos()
        {
            var r = new RectangleF(Width - 34 - 16, 1, 16, Height - 2);
            return r;
        }

        private void NullPaint()
        {
            using (var g = CreateGraphics())
            {
                var b = g.VisibleClipBounds;
                var r = new RectangleF(b.X + 1, b.Y + 1, b.Width - 34, b.Height - 2);
                g.FillRectangle(Brushes.White, r);
                g.DrawString("<Null>", Font, Brushes.Red, r);
            }
        }

        private void XPaint()
        {
            using (var g = CreateGraphics())
            {
                var r = GetXPos();
                g.FillRectangle(Brushes.LightGray, r);
                //g.DrawCenterMiddleString("X", Font, Brushes.Red, Rectangle.Round(r));
            }
        }

        private void CheckDoNull(Point p)
        {
            var r = GetXPos();
            if (r.Contains(p)) IsNull = true;
        }


        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            if (IsNull)
            {
                LockEventChanges = true;
                ControlValue = DateTime.Today;
                int gdt = 0; // NativeMethods.GDT_VALID;
                SystemTime sys = (SystemTime)DateTime.Today;
                var r = Rop.Winforms.Basic.Helper.SendMessage(this.Handle, DTM_GETMONTHCAL, IntPtr.Zero, IntPtr.Zero);
                var h = new IntPtr(r);
                SendMessage(h, DTM_SETSYSTEMTIME, gdt, ref sys);
                Invalidate();
                LockEventChanges = false;
            }

        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(
            IntPtr hWnd, // handle to destination window
            int msg, // message
            int wParam, // first message parameter
            ref SystemTime lParam); // second message parameter


        [StructLayout(LayoutKind.Sequential)]
        private struct SystemTime
        {
            ushort _year;
            ushort _month;
            ushort _dayOfWeek;
            ushort _day;
            ushort _hour;
            ushort _minute;
            ushort _second;
            ushort _milliseconds;

            public static implicit operator DateTime(SystemTime systemTime)
            {
                return new DateTime(
                    systemTime._year,
                    systemTime._month,
                    systemTime._day,
                    systemTime._hour,
                    systemTime._minute,
                    systemTime._second,
                    systemTime._milliseconds);
            }

            public static explicit operator SystemTime(DateTime dateTime)
            {
                SystemTime systemTime = new SystemTime
                {
                    _year = (ushort)dateTime.Year,
                    _month = (ushort)dateTime.Month,
                    _dayOfWeek = (ushort)dateTime.DayOfWeek,
                    _day = (ushort)dateTime.Day,
                    _hour = (ushort)dateTime.Hour,
                    _minute = (ushort)dateTime.Minute,
                    _milliseconds = (ushort)dateTime.Millisecond
                };

                return systemTime;
            }

        }

#pragma warning disable IDE0051 // Quitar miembros privados no utilizados
        private const int DTM_GETSYSTEMTIME = (0x1000 + 1),

            DTM_SETSYSTEMTIME = (0x1000 + 2),
            DTM_SETRANGE = (0x1000 + 4),
            DTM_SETFORMATA = (0x1000 + 5),
            DTM_SETFORMATW = (0x1000 + 50),
            DTM_SETMCCOLOR = (0x1000 + 6),
            DTM_GETMONTHCAL = (0x1000 + 8),
            DTM_SETMCFONT = (0x1000 + 9),
            DTS_UPDOWN = 0x0001,
            DTS_SHOWNONE = 0x0002,
            DTS_LONGDATEFORMAT = 0x0004,
            DTS_TIMEFORMAT = 0x0009,
            DTS_RIGHTALIGN = 0x0020,
            DTN_DATETIMECHANGE = ((0 - 760) + 1),
            DTN_USERSTRINGA = ((0 - 760) + 2),
            DTN_USERSTRINGW = ((0 - 760) + 15),
            DTN_WMKEYDOWNA = ((0 - 760) + 3),
            DTN_WMKEYDOWNW = ((0 - 760) + 16),
            DTN_FORMATA = ((0 - 760) + 4),
            DTN_FORMATW = ((0 - 760) + 17),
            DTN_FORMATQUERYA = ((0 - 760) + 5),
            DTN_FORMATQUERYW = ((0 - 760) + 18),
            DTN_DROPDOWN = ((0 - 760) + 6),
            DTN_CLOSEUP = ((0 - 760) + 7);
#pragma warning restore IDE0051 // Quitar miembros privados no utilizados
    }
}