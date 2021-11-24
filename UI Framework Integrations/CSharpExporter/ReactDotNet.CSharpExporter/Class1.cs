using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sdcb.TypeScript;
using Sdcb.TypeScript.TsTypes;

namespace ReactDotNet.CSharpExporter
{
    public static class MyParser
    {
        public static string GetCSharpDefinition(string tsCode)
        {
            var typeScriptAst = new TypeScriptAST(tsCode);

            
            var decleration = (TypeAliasDeclaration)typeScriptAst.OfKind(SyntaxKind.TypeAliasDeclaration).First();

            return string.Join(Environment.NewLine, Visit(decleration));

        }

        const string Padding = "    ";
        static IReadOnlyList<string> Visit(TypeAliasDeclaration typeAliasDeclaration)
        {
            return CreateList(add =>
            {
                add("[Enum(Emit.StringNameLowerCase)]");
                add($"public enum {typeAliasDeclaration.IdentifierStr}");
                add("{");

                var unionType = (UnionTypeNode)typeAliasDeclaration.OfKind(SyntaxKind.UnionType).First();

                add(Padding+string.Join(", ", CreateList(emitEnumName =>
                {
                    foreach (var typeNode in unionType.Types)
                    {
                        var text = ((StringLiteral)typeNode.First).Text;

                        emitEnumName(text);
                    }

                })));

                add("}");

            });
        }

        static IReadOnlyList<string> CreateList(Action<Action<string>> modify)
        {
            var list = new List<string>();

            modify(list.Add);

            return list;
        }
    }
}
