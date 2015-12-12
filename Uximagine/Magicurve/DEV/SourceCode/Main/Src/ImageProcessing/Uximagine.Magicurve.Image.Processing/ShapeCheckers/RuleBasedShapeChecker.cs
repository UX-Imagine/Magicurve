#region Imports
using System.Collections.Generic;
using System.Drawing;
using AForge;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing.Common;
using Uximagine.Magicurve.Image.Processing.Helpers;
#endregion

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    ///     The UI shape checker.
    /// </summary>
    public class RuleBasedShapeChecker : AdvancedShapeChecker
    {
        /// <summary>
        /// The text corner count.
        /// </summary>
        private const int TextCornerCount = 4;

        /// <summary>
        ///     The button corner count.
        /// </summary>
        private const int ButtonCornerCount = 4;

        /// <summary>
        ///     Determines whether the specified edge points is button.
        /// </summary>
        /// <param name="edgePoints">
        ///     The edge points.
        /// </param>
        /// <param name="corners">
        ///     The corners.
        /// </param>
        /// <returns>
        ///     <c> true</c> if [is button] shape.
        /// </returns>
        protected bool IsButton(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            var isButton = false;
            const double DistanceError = 30;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == ButtonCornerCount)
                {
                    //// get length of each side
                    var sides = new float[corners.Count];
                    var next = 1;
                    for (var i = 0; i < corners.Count; i++)
                    {
                        if (i == corners.Count - 1)
                        {
                            next = 0;
                        }

                        sides[i] = corners[i].DistanceTo(corners[next++]);
                    }

                    if (sides[0] - sides[2] < DistanceError && sides[1] - sides[3] < DistanceError)
                    {
                        isButton = true;
                    }
                }
            }

            return isButton;
        }

        /// <summary>
        ///     Determines whether [is drop down] [the specified edge points].
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <param name="corners">
        /// The corners.
        /// </param>
        /// <returns>
        ///     <c> true</c> if drop down.
        /// </returns>
        protected bool IsDropDown(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            var isDropDown = false;

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
        ///     Determines whether [is input text] [the specified edge points].
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="corners">The corners.</param>
        /// <returns>
        ///     <c>true</c> if [shape is input text].
        /// </returns>
        protected bool IsInputText(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            var isInputText = false;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == TextCornerCount)
                {
                    isInputText = true;
                }
            }

            return isInputText;
        }

        /// <summary>
        ///     Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        ///     The content type
        /// </returns>
        public override ControlType GetControlType(List<IntPoint> edgePoints)
        {
            var type = ControlType.None;

            var corners = this.GetShapeCorners(edgePoints);

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
            else if (this.IsRadioButton(edgePoints, corners))
            {
                type = ControlType.RadioButton;
            }

            return type;
        }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The control type.
        /// </returns>
        public override ControlType GetControlType(Bitmap source, List<IntPoint> edgePoints)
        {
            int horizontalCount;
            int verticalCount;

            ControlType type = ControlType.None;

            Bitmap image = source.Crop(edgePoints, 1);

            image = image.GetBlobReady();

            Bitmap vertical = (Bitmap)image.Clone();
            Bitmap horizontal = (Bitmap)image.Clone();

            vertical.ConvolutionFilter(FilterMatrix.Prewitt3x3Vertical);
            vertical.ToBinary();
            vertical.GetVerticalLinesCount(out verticalCount);

            horizontal.ConvolutionFilter(FilterMatrix.Prewitt3x3Horizontal);
            horizontal.ToBinary();
            horizontal.GetHorizontalLinesCount(out horizontalCount);

            if (this.IsValidControl(ConfigurationData.ParameterConfig.Button, horizontalCount, verticalCount))
            {
                type = ControlType.Button;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.InputPassword, horizontalCount, verticalCount))
            {
                type = ControlType.InputPassword;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.Image, horizontalCount, verticalCount))
            {
                type = ControlType.Image;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.CheckBox, horizontalCount, verticalCount))
            {
                type = ControlType.CheckBox;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.ComboBox, horizontalCount, verticalCount))
            {
                type = ControlType.ComboBox;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.DatePicker, horizontalCount, verticalCount))
            {
                type = ControlType.DatePicker;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.HorizontalLine, horizontalCount, verticalCount))
            {
                type = ControlType.HLine;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.HyperLink, horizontalCount, verticalCount))
            {
                type = ControlType.HyperLink;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.Iframe, horizontalCount, verticalCount))
            {
                type = ControlType.Iframe;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.Label, horizontalCount, verticalCount))
            {
                type = ControlType.Label;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.Patagraph, horizontalCount, verticalCount))
            {
                type = ControlType.Paragraph;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.Range, horizontalCount, verticalCount))
            {
                type = ControlType.Range;
            }
            else if (this.IsValidControl(ConfigurationData.ParameterConfig.InputText, horizontalCount, verticalCount))
            {
                type = ControlType.InputText;
            }
            else if (this.IsCircle(edgePoints))
            {
                type = ControlType.Range;
            }

            image.Dispose();
            vertical.Dispose();
            horizontal.Dispose();

            return type;
        }

        /// <summary>
        /// Determines whether the specified horizontal count is button.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <param name="horizontalCount">The horizontal count.</param>
        /// <param name="verticalCount">The vertical count.</param>
        /// <param name="cornerCount">The corner count.</param>
        /// <returns>
        ///   <c>true.</c> if is button. otherwise <c>false.</c>
        /// </returns>
        private bool IsValidControl(Feature feature, int horizontalCount, int verticalCount, int cornerCount = 0)
        {
            
            if (horizontalCount == feature.HorizontalCount && 
                verticalCount == feature.VerticalLineCount && 
                cornerCount == feature.CornersCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified edge points is RadioButton.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <param name="corners">
        /// The corners.
        /// </param>
        /// <returns>
        /// <c>True</c> if radio. otherwise <c>false</c>
        /// </returns>
        private bool IsRadioButton(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            return this.IsCircle(edgePoints);
        }
    }
}