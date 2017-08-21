using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebOptimizer.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddBundleContentTypeAnalyzer : AssetPipelineBaseAnalyzer
    {
        private static readonly DiagnosticDescriptor _descriptor = Descriptors.ContentTypeWrongFormat;
        public AddBundleContentTypeAnalyzer()
            : base(_descriptor, "AddBundle")
        { }

        protected override void Analyze(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, IMethodSymbol method)
        {
            var arguments = invocation.ArgumentList.Arguments;

            if (arguments.Count > 1)
            {
                var value = context.SemanticModel.GetConstantValue(arguments[1].Expression);

                if (value.HasValue && value.Value is string contentType && !IsValidContentType(contentType))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        _descriptor,
                        invocation.GetLocation(),
                        contentType));
                }
            }
        }

        private static bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
                return false;

            var slash = contentType.IndexOf("/");

            return slash > 0 && slash < contentType.Length - 1;
        }
    }
}
