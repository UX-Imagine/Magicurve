using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public interface IGenerator
    {
         string CreateHtmlCode(List<Control> controls);

         string CreateHeaderPart(string html,string body);

         string NormalStartTag(string tagName);

         string EndTag(string tagName);

        string DynamicContent();

        string InputTag(string inputType, string inputName, string inputValue);

        string InputButton(Control button);

        string InputCheckBox(Control checkbox);

        string InputRadio(Control radio);

        string ComboBox(Control combo);

        string InputText(Control text);

        string InputPassword(Control password);

        string InputDatePicker(Control datepicker);

        string TextBox(Control textbox); // for paragraph and normal single line label type text

        string TextArea(Control textarea);

        string ImageTag(Control img);

        string HyperLink(Control hyperlink);

        string Iframe(Control iframe);

        string HLine(Control hline);



       

        
    }
}
