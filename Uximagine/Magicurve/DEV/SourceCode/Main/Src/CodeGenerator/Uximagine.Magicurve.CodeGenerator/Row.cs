using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public class Row
    {
        public List<Control> Controls
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public int RowIndex
        {
            get;
            set;
        }
    }
}
