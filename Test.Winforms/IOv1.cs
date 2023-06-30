using System;

namespace Test.Winforms
{
    public interface IOv1
    {
        DateTime Fecha { get; }
        int Serial { get; }
    }
    public interface IOv2
    {
        string Secundario { get; }
    }
}