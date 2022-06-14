using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ProxyGenerator
{
    public partial class Class1<T> : IMyInterface<T>
    {
        public event EventHandler? ValueChanged;
        public event EventHandler<T>? TValueChanged;
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Fecha { get; } = DateTime.Now;
        public Class1(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string SacaAlgo(T valor, string otroparametro)
        {
            throw new NotImplementedException();
        }
        public void OnValueChanged()
        {
            throw new NotImplementedException();
        }
        public void OnIntValueChanged()
        {
            throw new NotImplementedException();
        }
    }
}
