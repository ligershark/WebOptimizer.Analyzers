using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WebOptimizer.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddBundleGlobbingRouteAnalyzer : AssetPipelineBaseAnalyzer
    {
        private static readonly DiagnosticDescriptor _descriptor = Descriptors.GlobbingRoutesNotAllowed;
        public AddBundleGlobbingRouteAnalyzer()
            : base(_descriptor, "AddBundle", "AddCssBundle", "AddJavaScriptBundle", "AddHtmlBundle")
        { }

        protected override void Analyze(SyntaxNodeAnalysisContext context, InvocationExpressionSyntax invocation, IMethodSymbol method)
        {
            SeparatedSyntaxList<ArgumentSyntax> arguments = invocation.ArgumentList.Arguments;

            if (arguments.Count > 0)
            {
                Optional<object> value = context.SemanticModel.GetConstantValue(arguments[0].Expression);

                if (value.HasValue && value.Value is string route && !IsValidRoute(route))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        _descriptor,
                        invocation.GetLocation(),
                        route));
                }
            }
        }

        private static bool IsValidRoute(string route)
        {
            if (string.IsNullOrEmpty(route))
                return false;

            return !route.Contains("*") && !route.Contains("[") && !route.Contains("?");
        }
    }
}
