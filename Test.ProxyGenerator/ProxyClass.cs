﻿using System.Runtime.CompilerServices;
using Rop.ProxyGenerator.Annotations;

namespace Test.ProxyGenerator;

[ProxyOf(typeof(IMyInterface<bool>), nameof(MyInterface), new string[] { "a", nameof(IMyInterface<int>.Fecha), "b" })]
public partial class ProxyClass: IMyInterface<bool>
{
    public IMyInterface<bool> MyInterface { get; } = new Class1<bool>("Hola", "Hello");
    public DateTime Fecha { get; set; }
}