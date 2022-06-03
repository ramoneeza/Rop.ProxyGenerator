# Rop.ProxyGenerator

Features
--------

Rop.ProxyGenerator is a source generator solution to automatic proxy of interfaces

`Rop.ProxyGenerator.Annotations` 
------

Interfaces to decorate a class to contain a proxy.

```csharp
[AttributeUsage(AttributeTargets.Class)]
public class ProxyOfAttribute:Attribute
{
...
    public ProxyOfAttribute(Type interfacename, string fieldname,string[] exclude=null)
    {
	...
    }
}
```

`Rop.ProxyGenerator`
------

The source generator that create the proxy as partial class.
Must be included as:

* OutputItemType="Analyzer" 
* ReferenceOutputAssembly="false"


 ------
 (C)2022 Ramón Ordiales Plaza
