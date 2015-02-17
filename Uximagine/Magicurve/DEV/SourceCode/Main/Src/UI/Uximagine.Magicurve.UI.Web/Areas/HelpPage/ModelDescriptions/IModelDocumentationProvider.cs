using System;
using System.Reflection;

namespace Uximagine.Magicurve.UI.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Model Documentation Provider Interface.
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <returns>
        /// The Documentation.
        /// </returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The documentation.
        /// </returns>
        string GetDocumentation(Type type);
    }
}