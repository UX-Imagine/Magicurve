using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Core.Shapes
{
    public class Paragraph: Control
    {

        public override ControlType Type
        {
            get
            {
                return ControlType.Paragraph;
            }
        }

        public string Content
        {
            get;
            set;
        }

    }
}
