using AForge;
using System.Collections.Generic;
using System.Drawing;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    public class ControlsChecker:AdvancedShapeChecker
    {
        public override ControlType GetControlType(List<IntPoint> edgePoints)
        {
            ControlType type = ControlType.None;

            List<IntPoint> corners = this.GetShapeCorners(edgePoints);

            if (this.CheckButton(edgePoints, corners))
            {
                type = ControlType.Button;
            }
            
            return type;
        }

        public override ControlType GetControlType(Bitmap control, List<IntPoint> edgePoints)
        {
            throw new System.NotImplementedException();
        }

        protected bool CheckButton(List<IntPoint> edgePoints, List<IntPoint> corners)
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
    }
}
