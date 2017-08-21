using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace WebOptimizer.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddBundleNoSourceFilesAnalyzer : AssetPipelineBaseAnalyzer
    {
        private static readonly DiagnosticDescriptor _descriptor = Descriptors.SourcesFilesMustBeSpecified;
        public AddBundleNoSourceFilesAnalyzer()
            : base(_descriptor, "AddBundle", "AddCssBundle", "AddJavaScriptBundle", "AddHtmlBundle")
        { }

        protected override void Analyze(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, IMethodSymbol method)
        {
            var arguments = invocation.ArgumentList.Arguments;

            if (method.Parameters.Length == 3 && arguments.Count == 2)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    _descriptor,
                    invocation.GetLocation(),
                    method.Name));
            }
        }
    }
}
