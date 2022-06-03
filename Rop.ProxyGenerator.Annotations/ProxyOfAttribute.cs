using System;
using System.Dynamic;

namespace Rop.ProxyGenerator.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ProxyOfAttribute:Attribute
    {
        public Type InterfaceName { get; }
        public string FieldName { get; }
        public string[] Exclude { get; }
        public ProxyOfAttribute(Type interfacename, string fieldname,string[] exclude=null)
        {
            InterfaceName=interfacename;
            FieldName=fieldname;
        }
    }
}
