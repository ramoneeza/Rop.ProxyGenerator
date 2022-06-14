using System;
using System.Dynamic;

namespace Rop.ProxyGenerator.Annotations
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class ProxyOfAttribute:Attribute
    {
        public string InterfaceName { get; }
        public string FieldName { get; }
        public string[] Exclude { get; }
        public ProxyOfAttribute(Type interfacename, string fieldname,string[] exclude=null)
        {
            InterfaceName=interfacename.ToString();
            FieldName=fieldname;
            Exclude=exclude;
        }
        public ProxyOfAttribute(string interfacename, string fieldname,string[] exclude=null)
        {
            InterfaceName=interfacename;
            FieldName=fieldname;
            Exclude=exclude;
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class OverrideWithPreBaseAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Method)]
    public class OverrideWithPostBaseAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
    public class OverrideNoBaseAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
    public class OverrideNewAttribute : Attribute { }

}
