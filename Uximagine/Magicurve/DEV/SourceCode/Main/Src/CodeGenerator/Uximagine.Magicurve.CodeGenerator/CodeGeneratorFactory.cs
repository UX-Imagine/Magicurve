using Uximagine.Magicurve.Core.Reflection;

namespace Uximagine.Magicurve.CodeGenerator
{
    /// <summary>
    /// Code generator Factory
    /// </summary>
    public static class CodeGeneratorFactory
    {
        public const string GeneratorImplementation = @"GenImpel";

        /// <summary>
        /// Gets the processing service.
        /// </summary>
        /// <returns>
        /// The code generator.
        /// </returns>
        public static IGenerator GetProcessingService()
        {
            return ObjectFactory.GetInstance<IGenerator>(
                GeneratorImplementation);
        }
    }
}
