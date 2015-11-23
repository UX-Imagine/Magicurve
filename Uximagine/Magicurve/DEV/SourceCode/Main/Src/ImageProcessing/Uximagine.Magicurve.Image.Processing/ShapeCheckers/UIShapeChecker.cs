#region Imports
using System.Collections.Generic;
using AForge;
using AForge.Math.Geometry;
using Uximagine.Magicurve.Core.Models; 
#endregion

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    ///     The UI shape checker.
    /// </summary>
    public class UiShapeChecker : AdvancedShapeChecker
    {
        /// <summary>
        /// The text corner count.
        /// </summary>
        private const int TEXT_CORNER_COUNT = 4;

        /// <summary>
        ///     The button corner count.
        /// </summary>
        private const int BUTTON_CORNER_COUNT = 4;

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

            const double distanceError = 30;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == BUTTON_CORNER_COUNT)
                {
                    //get length of each side
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

                    if (sides[0] - sides[2] < distanceError && sides[1] - sides[3] < distanceError)
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
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="corners">The corners.</param>
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
                if (corners.Count == TEXT_CORNER_COUNT)
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

            IntPoint minXy;
            IntPoint maxXy;
            PointsCloud.GetBoundingRectangle(edgePoints, out minXy, out maxXy);

            this.X = minXy.X;
            this.Y = minXy.Y;
            this.Height = maxXy.Y - minXy.Y;
            this.Width = maxXy.X - minXy.X;

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
            }else if (this.IsRadioButton(edgePoints, corners))
            {
                type = ControlType.RadioButton;
            }

            return type;
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