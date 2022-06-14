namespace Test.ProxyGenerator;

public interface IMyInterface<T>
{
    event EventHandler ValueChanged;
    event EventHandler<T> TValueChanged;
    void OnValueChanged();
    void OnIntValueChanged();
    string Name { get; set; }
    string Description { get; set; }
    DateTime Fecha { get; }
    string SacaAlgo(T valor, string otroparametro);
}