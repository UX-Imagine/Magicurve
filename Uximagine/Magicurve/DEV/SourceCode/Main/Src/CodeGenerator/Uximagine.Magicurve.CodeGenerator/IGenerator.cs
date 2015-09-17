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

         string GetHeader(string html,string body);

         string NormalStartTag(string tagName);

         string EndTag(string tagName);

        string DynamicContent();

        string InputTag(string inputType, string inputName, string inputValue);

        string GetButton(Control button);

        string GetCheckBox(Control checkbox);

        string GetRadio(Control radio);

        string GetCombo(Control combo);

        string GetText(Control text);

        string GetPassword(Control password);

        string GetDatePicker(Control datepicker);

        string GetTextBox(Control textbox); // for paragraph and normal single line label type text

        string GetTextArea(Control textarea);

        string GetImage(Control img);

        string GetHyperLink(Control hyperlink);

        string GetIframe(Control iframe);

        string GetHLine(Control hline);



       

        
    }
}
