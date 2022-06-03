using System.Runtime.CompilerServices;
using Rop.ProxyGenerator.Annotations;

namespace Test.ProxyGenerator;

[ProxyOf(typeof(IMyInterface), nameof(ProxyClass.MyInterface), new string[] { "a", nameof(IMyInterface.Fecha), "b" })]
public partial class ProxyClass : IMyInterface
{
    public IMyInterface MyInterface { get; } = new Class1("Holala", "Hello");

}