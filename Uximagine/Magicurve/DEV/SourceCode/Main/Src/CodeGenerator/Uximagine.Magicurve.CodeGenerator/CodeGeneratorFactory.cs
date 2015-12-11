using Uximagine.Magicurve.Core.Reflection;

namespace Uximagine.Magicurve.CodeGenerator
{
    using System;

    using Uximagine.Magicurve.Core.Models;

    /// <summary>
    /// Code generator Factory
    /// </summary>
    public static class CodeGeneratorFactory
    {
        /// <summary>
        /// The generator implementation.
        /// </summary>
        public const string GeneratorImplementation = @"GenImpel";

        /// <summary>
        /// Gets the processing service.
        /// </summary>
        /// <returns>
        /// The code generator.
        /// </returns>
        public static IGenerator GetCodeGenerator()
        {
            return ObjectFactory.GetInstance<IGenerator>(
                GeneratorImplementation);
        }

        /// <summary>
        /// The get code generator.
        /// </summary>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <returns>
        /// The <see cref="IGenerator"/>.
        /// </returns>
        public static IGenerator GetCodeGenerator(CodeStyle style)
        {
            switch (style)
            {
                case CodeStyle.Simple:
                    return new SimpleCodeGenerator();
                case CodeStyle.Responsive:
                    return new ResponsiveCodeGenerator();
                case CodeStyle.Ionic:
                    throw new NotImplementedException();
                default:
                    return new SimpleCodeGenerator();
            }
        }
    }
}
