using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    /// <summary>
    /// code generator class
    /// </summary>
    public class CodeGenerator : IGenerator
    {

        public string html = "html";
        public string title = "<title>";
        public string head = "<head>";
        public string body = "body";
        public string paraOrLabelTag = "p";
        public string formTag = "form";
        public string inputTag = "input";
        public string textInput = "text";
        public string submitInput = "submit";
        public string resetInput = "clear";
        public string passwordInput = "password";
        public string radioInput = "radio";
        public string checkboxInput = "checkbox";
        public string buttonInput = "button";
        public string dateInput = "date";
        public string textAreaTag = "textarea";
        public string imageTag = "img";
        public string hyperLinkTag = "a";
        public string iframeTag = "iframe";
        public string groupTag = "div";
        public string horizontalTag = "hr";

        public string CreateHtmlCode(List<Control> controls)
        {
            List<Control> control = new List<Control>();// control list with set of html controls and their properties

            //sort list (group using parent,sort using y and then finally sort using x
            //get item of sorted list using foreach
            //check control.type == "controlType" (button,checkbox etc) then call particular method using if else if blocks

            string header = CreateHeaderPart(html, body);

            return header;//return generated html code as string

        }

        public string CreateHeaderPart(string html, string body)
        {
            string header = NormalStartTag(html) + "\n" + NormalStartTag(body) + "\n";
            return header;
        }

        public string NormalStartTag(string tagName)
        {
            string startTag = "<" + tagName + ">"+"\n";
            return startTag;
        }

        public string EndTag(string tagName)
        {
            string endTag = "</" + tagName + ">" + "\n";
            return endTag;
        }

        public string DynamicContent()
        {
            return "";
        }

        public string InputTag(string inputType, string inputName, string inputValue)
        {
            return "";
        }

        public string InputButton(Control button)
        {
            return "";
        }

        public string InputCheckBox(Control checkbox)
        {
            return "";
        }

        public string InputRadio(Control radio)
        {
            return "";
        }

        public string ComboBox(Control combo)
        {
            return "";
        }

        public string InputText(Control text)
        {
            return "";
        }

        public string InputPassword(Control password)
        {
            return "";
        }

        public string InputDatePicker(Control datepicker)
        {
            return "";
        }

        public string TextBox(Control textbox) // for paragraph and normal single line label type text
        {
            return "";
        }

        public string TextArea(Control textarea)
        {
            return "";
        }

        public string ImageTag(Control img)
        {
            return "";
        }

        public string HyperLink(Control hyperlink)
        {
            return "";
        }

        public string Iframe(Control iframe)
        {
            return "";
        }

        public string HLine(Control hline)
        {
            return "";
        }



        public string CreateHtmlCode(string html, string body)
        {
            throw new NotImplementedException();
        }
    }
}
