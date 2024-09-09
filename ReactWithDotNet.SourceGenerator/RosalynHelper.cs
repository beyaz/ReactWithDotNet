namespace ReactWithDotNet;


using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

static class RoslynHelper
{
    public  static string GetNamespace(ClassDeclarationSyntax classDeclaration)
    {
        var fileScopedNamespaceName = classDeclaration.FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>()?.Name;
        if (fileScopedNamespaceName != null)
        {
            return fileScopedNamespaceName.ToString();
        }

        var namespaceDeclaration = classDeclaration.Ancestors()
            .OfType<NamespaceDeclarationSyntax>()
            .FirstOrDefault();

        return namespaceDeclaration?.Name.ToString() ?? "global";
    }
    /// <summary>
    /// Finds a class declaration by its name and namespace in the given compilation.
    /// </summary>
    /// <param name="compilation">The compilation to search through.</param>
    /// <param name="className">The name of the class to find.</param>
    /// <param name="namespaceName">The namespace of the class to find.</param>
    /// <returns>The ClassDeclarationSyntax if found, null otherwise.</returns>
    public static ClassDeclarationSyntax FindClassByNameAndNamespace(Compilation compilation, string className, string namespaceName)
    {
        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var root = syntaxTree.GetRoot();

            // Find all class declarations in the current syntax tree
            var classDeclarations = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>();

            // Loop through each class declaration and check its name and namespace
            foreach (var classDeclaration in classDeclarations)
            {
                // Check if the class name matches
                if (classDeclaration.Identifier.Text == className)
                {
                    

                    if (GetNamespace(classDeclaration) == namespaceName)
                    {
                        return classDeclaration; // Class found
                    }
                }
            }
        }

        return null; // Class not found
    }
}


