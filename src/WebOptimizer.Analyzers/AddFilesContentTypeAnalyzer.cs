using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebOptimizer.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddFilesContentTypeAnalyzer : AssetPipelineBaseAnalyzer
    {
        private static readonly DiagnosticDescriptor _descriptor = Descriptors.ContentTypeWrongFormat;
        public AddFilesContentTypeAnalyzer()
            : base(_descriptor, "AddFiles")
        { }

        protected override void Analyze(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, IMethodSymbol method)
        {
            SeparatedSyntaxList<ArgumentSyntax> arguments = invocation.ArgumentList.Arguments;

            if (arguments.Count > 0)
            {
                ArgumentSyntax arg = arguments[0];
                Optional<object> value = context.SemanticModel.GetConstantValue(arg.Expression);

                if (value.HasValue && value.Value is string contentType && !IsValidContentType(contentType))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        _descriptor,
                        arg.GetLocation(),
                        contentType));
                }
            }
        }

        private static bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
                return false;

            int slash = contentType.IndexOf("/");

            return slash > 0 && slash < contentType.Length - 1;
        }
    }
}
