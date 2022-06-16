﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rop.ProxyGenerator
{
    public static partial class SyntaxHelper
    {
       /// <summary>
        /// Childs of type T
        /// </summary>
        public static IEnumerable<T> ChildNodesOfType<T>(this SyntaxNode node) => node.ChildNodes().OfType<T>();

       public static IEnumerable<SyntaxNode> ChildNodesOfType(this SyntaxNode node,params Type[] types)
        {
            return node.ChildNodes().Where(n => types.Any(t=>IsSameOrSubclass(t,n.GetType())));
        }
        public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
        }

        public static bool IsStatic(this ClassDeclarationSyntax cds)
        {
           return cds.Modifiers.Any(SyntaxKind.StaticKeyword);
        }

        public static TypeName GetInterfaceImplementation(this ClassDeclarationSyntax cds, TypeName originalinterface)
        {
            if (!originalinterface.IsGeneric) return originalinterface;
            foreach (var basetype in cds.BaseList.Types)
            {
                var n = basetype.ToString();
            }
            return originalinterface;
        }

        public static string ToStringValue(this ExpressionSyntax expression)
        {
            switch (expression)
            {
                case TypeOfExpressionSyntax tof:
                    return tof.Type.ToString();
                case InvocationExpressionSyntax inv:
                    var a = inv.ArgumentList.Arguments.FirstOrDefault() as ArgumentSyntax;
                    IdentifierNameSyntax i=null;
                    if (a?.Expression is IdentifierNameSyntax aasi)
                    {
                        i = aasi;
                    }
                    else
                    {
                        var b =  a?.Expression as MemberAccessExpressionSyntax;
                        i = b?.ChildNodes().Last() as IdentifierNameSyntax;
                    }
                    return i?.Identifier.ToString() ?? "";
                case ArrayCreationExpressionSyntax arr:
                    var arrv = arr.Initializer.Expressions.Select(c => c.ToStringValue());
                    return string.Join(",", arrv);
                default:
                    var v= expression.ToString();
                    if (v.StartsWith("\"")) v = v.Substring(1);
                    if (v.EndsWith("\"")) v = v.Substring(0,v.Length-1);
                    return v;
            }
        }
        public static IEnumerable<string> ToStringValues(this AttributeArgumentListSyntax argumentlist)
        {
            foreach (var arg in argumentlist.Arguments)
            {
                yield return arg.Expression.ToStringValue();
            }
        }

        public static IEnumerable<(string, string)> GetUsings(this SyntaxTree syntaxTree)
        {
            var r = syntaxTree.GetRoot();
            return r.ChildNodesOfType<UsingDirectiveSyntax>().Select(u => (u.Name.ToString(), u.ToString())).ToList();
        }
        public static string GetNamespace(this SyntaxTree syntaxTree){
            var r = syntaxTree.GetRoot();
            return r.ChildNodesOfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString();
        }

        public static IEnumerable<ISymbol> GetOrderedMembers(this INamedTypeSymbol typeSymbol,bool inherited)
        {
            var morder = new List<string>();
            var dic=new Dictionary<string,ISymbol>();
            _getOrderedMembers(typeSymbol,inherited,morder,dic);
            //var mnames = morder.ToImmutableHashSet();
            //var dic=typeSymbol.GetMembers().Where(mm => mm.Name.InList(mnames)).ToDictionary(mm=>mm.Name);
            foreach (var m in morder)
            {
                yield return dic[m];
            }
        }

        private static void _getOrderedMembers(this INamedTypeSymbol typeSymbol, bool inherited,List<string> morder,Dictionary<string,ISymbol> dic)
        {
            if (inherited)
            {
                var sub = typeSymbol.Interfaces;
                foreach (var s in sub)
                {
                    _getOrderedMembers(s,true, morder,dic);
                }
            }
            var singlemorder = typeSymbol.MemberNames;
            morder.AddRange(singlemorder);
            var mnames = morder.ToImmutableHashSet();
            foreach (var m in typeSymbol.GetMembers())
            {
                if (!m.Name.InList(mnames)) continue;
                dic[m.Name] = m;
            }
        }
    }
}
