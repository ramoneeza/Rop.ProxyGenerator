using System.ComponentModel;
using Rop.ProxyGenerator.Annotations;

namespace Test.ProxyGenerator;

[ProxyOf(typeof(IDisposable), nameof(MyInterface3))]
public partial class ProxyClass3 : IDisposable
{
    public IDisposable MyInterface3 { get; }

    public ProxyClass3(IDisposable myInterface)
    {
        MyInterface3 = myInterface;
    }
}

public abstract class OneClase3
{
    public virtual int MyMethod(int a, int b)
    {
        return a + b;
    }
    public virtual int MyMethod2(int a, int b)
    {
        return a - b;
    }
    protected abstract int MyMethod3(int a, int b);
}

[ProxyOf(typeof(IOv1), nameof(OneClase4.Ovi))]
public partial class OneClase4:OneClase3, IOv1
{
    public IOv1 Ovi { get; }
    public OneClase4(IOv1 ovi)
    {
        Ovi = ovi;
    }
}

public interface IOv1
{
    [OverrideWithPreBase]
    int MyMethod(int a,int b);
    [OverrideWithPostBase]
    int MyMethod2(int a,int b);
    [ExplicitOverrideNoBase]
    int MyMethod3(int a,int b);
    [ExplicitOverrideNoBase]
    int MyMethod4(int a,int b);
    int MyMethod5();
    [IncludeNextAttributes]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    int MyMethod6();
}
