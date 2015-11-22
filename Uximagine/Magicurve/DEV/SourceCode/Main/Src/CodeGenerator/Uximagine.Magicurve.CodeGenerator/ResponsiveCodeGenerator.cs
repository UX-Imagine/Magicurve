using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public class ResponsiveCodeGenerator : IGenerator
    {
        public string boostrapCss = "css/bootstrap.min.css";
        public string boostrapMinJs = "js/bootstrap.min.js";
        public string jquery = "js/jquery.js";
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
        public string groupTag = "div";
        public string horizontalTag = "hr";
        public string div = "div";
        public string newline = Environment.NewLine;

        SortHelper sortHelper = new SortHelper();

        public string CreateHtmlCode(List<Control> controls)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(NormalStartTag(html));
            builder.Append(NormalStartTag(head));
            builder.Append(ApplyCss(boostrapCss));
            builder.Append(EndTag(head));
            builder.Append(GetBody());

            //List<Control> sortedControls = sortHelper.SortListYProperty(controls);
            List<Row> sortedControls = sortHelper.DivAlgorithm(sortHelper.SortListYProperty(controls));
            List<Row> finalRowListControls = sortHelper.SortListXProperty(sortedControls);

            //execute sorted list and check control types
            foreach (Row row in finalRowListControls)
            {
                //openrow(row.Height)
                builder.Append(OpenRowDiv(row.Height));

                foreach (Control item in row.Controls)
                {
                    //openColums(row.controls.Count)
                    builder.Append(OpenColDiv(row.Controls.Count));

                //Console.Write(((Button)con).Name);
                    switch (item.Type)
                    {
                        case Core.Models.ControlType.Button:
                            builder.Append(this.GetButton(item));
                            break;

                        case Core.Models.ControlType.CheckBox:
                            builder.Append(this.GetCheckBox(item));
                            break;

                        case Core.Models.ControlType.RadioButton:
                            builder.Append(this.GetRadio(item));
                            break;

                        case Core.Models.ControlType.ComboBox:
                            builder.Append(this.GetCombo(item));
                            break;

                        case Core.Models.ControlType.InputText:
                            builder.Append(this.GetText(item));
                            break;

                        case Core.Models.ControlType.InputPassword:
                            builder.Append(this.GetPassword(item));
                            break;

                        case Core.Models.ControlType.DatePicker:
                            builder.Append(this.GetDatePicker(item));
                            break;

                        case Core.Models.ControlType.Paragraph:
                            builder.Append(this.GetPara(item));
                            break;

                        case Core.Models.ControlType.Label:
                            builder.Append(this.GetLabel(item));
                            break;

                        case Core.Models.ControlType.TextArea:
                            builder.Append(this.GetTextArea(item));
                            break;

                        case Core.Models.ControlType.Image:
                            builder.Append(this.GetImage(item));
                            break;

                        case Core.Models.ControlType.HyperLink:
                            builder.Append(this.GetHyperLink(item));
                            break;

                        case Core.Models.ControlType.Iframe:
                            builder.Append(this.GetHyperLink(item));
                            break;

                        case Core.Models.ControlType.HLine:
                            builder.Append(this.GetHLine(item));
                            break;

                    }
                    //close col div
                    builder.Append(EndTag(div));
                }
                //close row div
                builder.Append(EndTag(div));
            }

            builder.Append(ApplyScript(jquery));
            builder.Append(ApplyScript(boostrapMinJs));
            builder.Append(GetFooter());

            return builder.ToString();//return generated html code as string

        }

        private string GetFooter()
        {
            string footer = string.Format(@"</div> </body>"+newline+"</html>");
            return footer;
        }

        //public string GetHeader(string href)
        //{
        //    string header = string.Format(@"<html>"+newline+"<head>"+newline+"<link href='{0}' rel='stylesheet'>"+newline+"</head>"+newline,href);
        //    return header;
        //}

        public string GetBody()
        {
            string body = string.Format(@"<body>" + newline + "<div class=container style='margin-top: 10px;'>" + newline);
            return body;
        }

        public string ApplyCss(string src)
        {
            string content = string.Format(@"<link rel='stylesheet' type='text/css' href='{0}'>" + newline, src);
            return content;
        }

        public string ApplyScript(string src)
        {
            string script = string.Format(@"<script src='{0}'></script>" + newline, src);
            return script;
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
            string div = string.Format(@" <div style='margin-left:{0}px;margin-top:{1}px' />" +newline, left, top);
            return div;
        }

        public string OpenRowDiv(double height)
        {
            string rowDiv = string.Format(@"<div class='row' style='height: {0}px'>" +newline, height);
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

            //string divTag = codegenerator.StartDiv(button.X, button.Y);
           //// string btn = string.Format(@"    <input type='button' value='{0}' style='left:{1}px;top:{2}px;position:absolute'/>"+newline,button.Value, button.X,button.Y);
            string btn = string.Format(@"    <input type='button' value='{0}' style='margin-left: {1}px'/>" + newline,button.Value, button.X);
            //string endDiv = codegenerator.EndTag(div);
            //return divTag + btn + endDiv;
            return btn;
        }

        public string GetCheckBox(Control checkbox)
        {
            string check = string.Format(@"    <input type='checkbox' style='width: {0}px;margin-left: {1}px'/>" + newline, checkbox.Width, checkbox.X);
           
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
   </select>" + newline, combo.X, combo.Y);

            return select;
        }

        public string GetText(Control text)
        {
            string txt = string.Format(@"    <input type='text' style='margin-left:{0}px'/>" + newline,
                                          text.X);
            
            return txt;
        }

        public string GetPassword(Control password)
        {
            string pass = string.Format(@"    <input type='password' style='margin-left:{0}px'/>",
                                           password.X);
            return pass;
        }

        public string GetDatePicker(Control datepicker)
        {
            string date = string.Format(@"    <input type='date' style='left:{0}px;top:{1}px'/>",
                                          datepicker.X,
                                          datepicker.Y);
            return date;
        }

        public string GetPara(Control para) // for paragraph 
        {
            //string paragraph = 
            return string.Empty;
        }

        public string GetLabel(Control control)
        {
            Label label = control as Label;
            string lbl = string.Format(@" <label style='margin-left:{0}px'>{1}</label>"+newline,label.X,label.Value);
            return lbl;
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
