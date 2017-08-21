using Microsoft.CodeAnalysis;

namespace WebOptimizer.Analyzers
{
    internal static class Descriptors
    {
        public static DiagnosticDescriptor SourcesFilesMustBeSpecified => new DiagnosticDescriptor("WO1000",
            "At least one source file must be specified",
            "At least one source file must be specified",
            "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static DiagnosticDescriptor GlobbingRoutesNotAllowed => new DiagnosticDescriptor("WO1001",
            "Globbing pattern is not allowed when specifying a route for a bundle",
            "\"{0}\" is invalid as a route for a bundle.",
            "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);

        public static DiagnosticDescriptor ContentTypeWrongFormat => new DiagnosticDescriptor("WO1002",
            "The content type is not a valid content type",
            "\"{0}\" is not a valid content type.",
            "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);
    }
}
