using System.Collections.Generic;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public interface IGenerator
    {
         string CreateHtmlCode(List<Control> controls, double width);

        // string GetHeader(string href);

         string GetBody();

         string ApplyCss(string src);

         string ApplyScript(string src);

         string NormalStartTag(string tagName);

         string EndTag(string tagName);

        string DynamicContent();

        string StartDiv(double left, double top);

        string OpenRowDiv(double height);

        string OpenColDiv(int controlsCount);

        string InputTag(string inputType, string inputName, string inputValue);

        string GetButton(Control button);

        string GetCheckBox(Control checkbox);

        string GetRadio(Control radio);

        string GetCombo(Control combo);

        string GetText(Control text);

        string GetPassword(Control password);

        string GetDatePicker(Control datepicker);

        string GetPara(Control para); // for paragraph and normal single line label type text

        string GetLabel(Control label);

        string GetTextArea(Control textarea);

        string GetImage(Control img);

        string GetHyperLink(Control hyperlink);

        string GetIframe(Control iframe);

        string GetHLine(Control hline);



       

        
    }
}
