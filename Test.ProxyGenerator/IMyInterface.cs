namespace Test.ProxyGenerator;

public interface IMyInterface
{
    event EventHandler ValueChanged;
    event EventHandler<int> IntValueChanged;
    void OnValueChanged();
    void OnIntValueChanged();
    string Name { get; set; }
    string Description { get; set; }
    DateTime Fecha { get; }
    string SacaAlgo(int valor, string otroparametro);
}