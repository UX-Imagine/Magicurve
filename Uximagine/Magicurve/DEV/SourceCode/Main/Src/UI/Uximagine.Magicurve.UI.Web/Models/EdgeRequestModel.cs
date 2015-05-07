namespace Uximagine.Magicurve.UI.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The edge request model.
    /// </summary>
    public class EdgeRequestModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EmailAddress]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        public int Range { get; set; }
    }
}