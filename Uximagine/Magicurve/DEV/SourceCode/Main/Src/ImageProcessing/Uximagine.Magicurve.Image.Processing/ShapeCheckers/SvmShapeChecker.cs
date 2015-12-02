using System;
using System.Collections.Generic;
using System.Drawing;
using AForge;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    public class SvmShapeChecker : IShapeChecker
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public ControlType GetControlType(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        public List<IntPoint> GetShapeCorners(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        public ControlType GetControlType(Bitmap original, List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        public void SetProperties(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }
    }
}
