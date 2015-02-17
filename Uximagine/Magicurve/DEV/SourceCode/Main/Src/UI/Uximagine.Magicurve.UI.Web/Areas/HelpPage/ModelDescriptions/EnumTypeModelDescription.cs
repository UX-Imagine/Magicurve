using System.Collections.ObjectModel;

namespace Uximagine.Magicurve.UI.Web.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// ENUM Type Model Description.
    /// </summary>
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTypeModelDescription"/> class.
        /// </summary>
        public EnumTypeModelDescription()
        {
            this.Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}