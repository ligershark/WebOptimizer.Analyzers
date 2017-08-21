using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebOptimizer.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddFilesNoSourceFilesAnalyzer : AssetPipelineBaseAnalyzer
    {
        private static readonly DiagnosticDescriptor _descriptor = Descriptors.SourcesFilesMustBeSpecified;
        public AddFilesNoSourceFilesAnalyzer()
            : base(_descriptor, "AddFiles")
        { }

        protected override void Analyze(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, IMethodSymbol method)
        {
            SeparatedSyntaxList<ArgumentSyntax> arguments = invocation.ArgumentList.Arguments;

            if (arguments.Count == 1)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    _descriptor,
                    invocation.GetLocation(),
                    method.Name));
            }
        }
    }
}
