using System.Collections.Generic;
using AForge;
using Uximagine.Magicurve.Core.Models;


namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The five cornered shape checker.
    /// </summary>
    public class FiveCornerShapeChecker : AdvancedShapeChecker
    {
        /// <summary>
        /// Determines whether the specified edge points is button.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <param name="corners">
        /// The corners.
        /// </param>
        /// <returns>
        /// <c> true</c> if [is button] shape.
        /// </returns>
        protected bool IsButton(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            bool isButton = false;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == 5)
                {
                    isButton = true;
                }
            }

            return isButton;
        }

        /// <summary>
        /// Determines whether [is drop down] [the specified edge points].
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="corners">The corners.</param>
        /// <returns>
        /// <c> true</c> if drop down.
        /// </returns>
        protected bool IsDropDown(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            bool isDropDown = false;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == 6)
                {
                    isDropDown = true;
                }
            }

            return isDropDown;
        }

        /// <summary>
        /// Determines whether [is input text] [the specified edge points].
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="corners">The corners.</param>
        /// <returns>
        /// <c>true</c> if [shape is input text].
        /// </returns>
        protected bool IsInputText(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            bool isInputText = false;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == 7)
                {
                    isInputText = true;
                }
            }

            return isInputText;
        }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The content type
        /// </returns>
        public override ControlType GetControlType(List<IntPoint> edgePoints)
        {
            ControlType type = ControlType.None;

            List<IntPoint> corners = this.GetShapeCorners(edgePoints);

            if (this.IsButton(edgePoints, corners))
            {
                type = ControlType.Button;
            }
            else if (this.IsDropDown(edgePoints, corners))
            {
                type = ControlType.ComboBox;
            }
            else if (this.IsInputText(edgePoints, corners))
            {
                type = ControlType.InputText;
            }

            return type;
        }
    }
}
