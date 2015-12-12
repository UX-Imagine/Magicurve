using System;
using System.Collections.Generic;
using System.Drawing;
using AForge;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Neuro.Processing;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The shape checker implemented using PCA classifier.
    /// </summary>
    public class MachineShapeChecker : AdvancedShapeChecker
    {
        /// <summary>
        /// The sample size.
        /// </summary>
        private const int SampleSize = 32;

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The content type
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// This shape checker does not implement this method.
        /// </exception>
        public override ControlType GetControlType(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The control type.
        /// </returns>
        public override ControlType GetControlType(Bitmap original, List<IntPoint> edgePoints)
        {
            ControlType type = ControlType.None;

            this.SetProperties(edgePoints);

            using (Bitmap cropped = original.Vectorize(edgePoints, 1, SampleSize))
            {
                IClassifier classifier = ProcessingFactory.GetClassifier();

                if (classifier.IsTrained == false)
                {
                    Trainer trainer = new Trainer();
                    trainer.Train(ConfigurationData.MinSize, 32);
                }

                int decision = classifier.Compute(cropped);
                
                type = (ControlType)(decision + 1);

               // cropped.Save("E:/Data/identified/cropped" + type + ".jpg");
            }
            
            return type;
        }
    }
}
