using Rop.ProxyGenerator.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Winforms
{
    [ProxyOfDisableNullable]
    [ProxyOf(typeof(IOv1), nameof(OneClase.Ovi),new string[] { "a", "Fecha", "b" })]
    [ProxyOf(typeof(IOv2), nameof(OneClase.Ovi2))]
    internal partial class OneClase:IOv1,IOv2
    {
        public IOv1 Ovi { get; set; }
        public IOv2 Ovi2 { get; set; }
        public DateTime Fecha { get; set; }

        public void Method1()
        {
            var a = this.Secundario;
        }

    }
}
