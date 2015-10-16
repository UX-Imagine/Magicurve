using System.Collections.Generic;
using AForge;
using Uximagine.Magicurve.Core.Models;
using AForge.Math.Geometry;


namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The five cornered shape checker.
    /// </summary>
    public class UiShapeChecker : AdvancedShapeChecker
    {
        /// <summary>
        /// The button corner count.
        /// </summary>
        private const int BUTTON_CORNER_COUNT = 4;        

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

            const double distanceError = 3;

            //var baseBuilder = new RulesEngine.Fluent.FluentBuilder();
            //baseBuilder.For<Foo1>()
            //            .Setup(f => f.Value)
            //                .MustBeGreaterThan(0);
            //baseBuilder.For<Foo2>()
            //            .Setup(f => f.Value)
            //                .MustEqual(6);

            //var baseEngine = baseBuilder.Build();
            //Assert.IsFalse(baseEngine.Validate(new Foo1(-1)));
            //Assert.IsFalse(baseEngine.Validate(new Foo2(1)));

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == UiShapeChecker.BUTTON_CORNER_COUNT)
                {
                    //get length of each side
                    float[] sides = new float[corners.Count];
                    int next = 1;
                    for (int i = 0; i < corners.Count; i++)
			        {
                        if (i == corners.Count - 1)
	                        {
		                        next = 0;
	                        }

			            sides[i] = corners[i].DistanceTo(corners[next++]);                        
			        }
                    
                    if (sides[0]-sides[2] < distanceError && sides[1] - sides[3] < distanceError)
	                {
	                    IntPoint minXY;
	                    IntPoint maxXY;
	                    PointsCloud.GetBoundingRectangle( edgePoints, out minXY, out maxXY );

                        this.X = minXY.X;
                        this.Y = minXY.Y;
                        this.Height = maxXY.Y - minXY.Y;
                        this.Width = maxXY.X - minXY.X;

		                isButton = true;
	                }
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
