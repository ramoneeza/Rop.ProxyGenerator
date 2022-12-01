using Rop.ProxyGenerator.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Winforms
{
    [ProxyOf(typeof(IOv1), nameof(OneClase.Ovi),new string[] { "a", "Fecha", "b" })]
    internal partial class OneClase:IOv1
    {
        public IOv1 Ovi { get; set; }
        public DateTime Fecha { get; set; }
    }
}
