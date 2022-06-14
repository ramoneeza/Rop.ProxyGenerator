using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rop.ProxyGenerator
{
    public class ProxyClassToAugment
    {
        public PartialClassToAugment ClassToAugment { get; set; }
        public InterfaceToProxy InterfaceToProxy { get; private set; }
        public ProxyClassToAugment(ClassDeclarationSyntax classToAugment)
        {
            ClassToAugment = new PartialClassToAugment(classToAugment);
            var att = classToAugment.GetDecoratedWith("ProxyOf");
            InterfaceToProxy = InterfaceToProxy.Factory(classToAugment);
        }
    }

    public class InterfaceToProxy
    {
        public TypeName InterfaceName { get; set; }
        public string FieldName { get; set; }
        public ImmutableHashSet<string> Excludes { get; set; }
        public string[] GenericNames { get; set; } = Array.Empty<string>();

        private InterfaceToProxy(TypeName interfacename, string fieldname, string excludesstr)
        {
            InterfaceName = interfacename;
            FieldName = fieldname;
            var exc = excludesstr.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            Excludes = exc.ToImmutableHashSet();
        }

        public static InterfaceToProxy Factory(ClassDeclarationSyntax classToAugment)
        {
            var att = classToAugment.GetDecoratedWith("ProxyOf");
            var values = att.ArgumentList.ToStringValues().ToList();
            if (values.Count < 2 || values.Count > 3) return null;
            var tipo =new TypeName(values[0]);
            var field = values[1];
            var excludes = (values.Count == 3) ? values[2] : "";
            return new InterfaceToProxy(tipo, field, excludes);
        }
    }
}