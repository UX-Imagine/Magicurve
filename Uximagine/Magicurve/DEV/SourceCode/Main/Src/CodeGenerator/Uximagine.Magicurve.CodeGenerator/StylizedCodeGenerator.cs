using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    /// <summary>
    /// code generator class
    /// </summary>
    public class StylizedCodeGenerator : IGenerator
    {

        public string customCss = "selected-css.css";
        public string html = "html";
        public string title = "title";
        public string head = "head";
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
        public string div = "div";
        public string horizontalTag = "hr";
        public string newline = Environment.NewLine;

        public string CreateHtmlCode(List<Control> controls, double width)
        {
            StringBuilder builder = new StringBuilder();
            // use passed control list with set of html controls and their properties

            //sort list (group using parent,sort using y and then finally sort using x
            //get item of sorted list using foreach
            //check control.type == "controlType" (button,checkbox etc) then call particular method using if else if blocks

            builder.Append(NormalStartTag(html));
            builder.Append(NormalStartTag(head));
            //builder.Append(ApplyCss(boostrapCss));
            builder.Append(ApplyCss(customCss));
            builder.Append(EndTag(head));
            builder.Append(GetBody());

            //sorting input list
            var query =
                from con in controls
                orderby con.Y
                select con;

            //execute sorted list and check control types
            foreach (var con in query)
            {


                //Console.Write(((Button)con).Name);

                switch (con.Type)
                {
                    case Core.Models.ControlType.Button:
                        builder.Append(this.GetButton(con));
                        break;

                    case Core.Models.ControlType.CheckBox:
                        builder.Append(this.GetCheckBox(con));
                        break;

                    case Core.Models.ControlType.RadioButton:
                        builder.Append(this.GetRadio(con));
                        break;

                    case Core.Models.ControlType.ComboBox:
                        builder.Append(this.GetCombo(con));
                        break;

                    case Core.Models.ControlType.InputText:
                        builder.Append(this.GetText(con));
                        break;

                    case Core.Models.ControlType.InputPassword:
                        builder.Append(this.GetPassword(con));
                        break;

                    case Core.Models.ControlType.DatePicker:
                        builder.Append(this.GetDatePicker(con));
                        break;

                    case Core.Models.ControlType.Paragraph:
                        builder.Append(this.GetPara(con));
                        break;

                    case Core.Models.ControlType.Label:
                        builder.Append(this.GetLabel(con));
                        break;

                    case Core.Models.ControlType.TextArea:
                        builder.Append(this.GetTextArea(con));
                        break;

                    case Core.Models.ControlType.Image:
                        builder.Append(this.GetImage(con));
                        break;

                    case Core.Models.ControlType.HyperLink:
                        builder.Append(this.GetHyperLink(con));
                        break;

                    case Core.Models.ControlType.Iframe:
                        builder.Append(this.GetHyperLink(con));
                        break;

                    case Core.Models.ControlType.HLine:
                        builder.Append(this.GetHLine(con));
                        break;

                }
            }

            builder.Append(GetFooter());

            return builder.ToString();//return generated html code as string

        }

        private string GetFooter()
        {
            string footer = string.Format(@"</body>"+newline+"</html>");
            return footer;
        }

        //public string GetHeader(string href)
        //{
        //    string header = string.Format(@"<html>" + newline + "<head>" + newline + "<link href='{0}' rel='stylesheet'>" + newline + "</head>" + newline, href);
        //    return header;
        //}

        public string GetBody()
        {
            string body = string.Format(@"<body>" + newline + "<div class=container>" + newline);
            return body;
        }

        public string ApplyCss(string src)
        {
            string content = string.Format(@"<link rel='stylesheet' type='text/css' href='{0}'>"+newline,src);
            return content;
        }

        public string ApplyScript(string src)
        {
            //string script = string.Format(@"<script src='{0}'></script>" + newline, src);
            return string.Empty;
        }

        public string NormalStartTag(string tagName)
        {
            string startTag = "<" + tagName + ">" + newline;
            return startTag;
        }

        public string EndTag(string tagName)
        {
            string endTag = "</" + tagName + ">" + "\n";
            return endTag;
        }

        public string DynamicContent()
        {
            return string.Empty;
        }

        public string StartDiv(double left, double top)
        {
            return string.Empty;
        }

        public string OpenRowDiv(double height)
        {
            string rowDiv = string.Format(@"<div class='row' style='height: {0}px'>" + height + newline);
            return rowDiv;
        }

        public string OpenColDiv(int controlsCount)
        {
            int colDivider = 12 / controlsCount;
            string colDiv = string.Format(@"<div class='col-md-{0}'>" + newline, colDivider);
            return colDiv;
        }

        public string InputTag(string inputType, string inputName, string inputValue)
        {
            return string.Empty;
        }

        public string GetButton(Control control)
        {
            Button button = control as Button;

            string btn = string.Format(@"    <input type='button' value='{0}' style='left:{1}px;top:{2}px;position:absolute'/>" + newline, button.Value, button.X, button.Y);
            return btn;
        }

        public string GetCheckBox(Control checkbox)
        {
            
            string check = string.Format(@"    <div class = 'squaredTwo' style='width:{0}px;height:{1}px;left:{2}px;top:{3}px'>" + newline + "<input type='checkbox' value='None' id='squaredTwo' name='check'/>" + newline + "<label for='squaredTwo'></label>" + newline + "</div>", checkbox.Width, checkbox.Height, checkbox.X, checkbox.Y);
            return check;
        }

        public string GetRadio(Control radio)
        {
            return string.Empty;
        }

        public string GetCombo(Control combo)
        {
            string select = string.Format(
                            @"    <select id='styledSelect' class='blueText' style='left:{0}px;top:{1}px;position:absolute'>
        <option value='yourMom'>Your Mom</option>
        <option value='myMom'>My Mom</option>
   </select>"+newline,combo.X,combo.Y);

            return select;
        }

        public string GetText(Control text)
        {
            string txt = string.Format(@"    <input type='text' style='left:{0}px;top:{1}px;position:absolute'/>"+newline,
                                           text.X,
                                           text.Y);
            return txt;
        }

        public string GetPassword(Control password)
        {
            string pass = string.Format(@"    <input type='password' style='left:{0};top:{1}'/>",
                                           password.X,
                                           password.Y);
            return pass;
        }

        public string GetDatePicker(Control datepicker)
        {
            string date = string.Format(@"    <input type='date' style='left:{0};top:{1}'/>",
                                          datepicker.X,
                                          datepicker.Y);
            return date;
        }

        public string GetPara(Control para) // for paragraph and normal single line label type text
        {
            return string.Empty;
        }

        public string GetLabel(Control label)
        {
            return string.Empty;
        }

        public string GetTextArea(Control textarea)
        {
            return string.Empty;
        }

        public string GetImage(Control img)
        {
            return "";
        }

        public string GetHyperLink(Control hyperlink)
        {
            return string.Empty;
        }

        public string GetIframe(Control iframe)
        {
            return string.Empty;
        }

        public string GetHLine(Control hline)
        {
            return string.Empty;
        }

    }
}
