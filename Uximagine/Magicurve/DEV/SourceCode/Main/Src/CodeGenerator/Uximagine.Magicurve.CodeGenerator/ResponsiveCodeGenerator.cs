using System;
using System.Collections.Generic;
using System.Text;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public class ResponsiveCodeGenerator : IGenerator
    {
        public string boostrapCss = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css";
        public string boostrapMinJs = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js";
        public string jquery = "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js";
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

        public string CreateHtmlCode(List<Control> controls, double width)
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
                builder.Append(OpenRowDiv(row.Height,row.TopMargin));

                foreach (Control item in row.Controls)
                {
                    //openColums(row.controls.Count)
                    //builder.Append(OpenColDiv(row.Controls.Count));
                    //builder.Append(OpenColDiv(sortHelper.GenerateColSizeAlgo(item,width),item));
                    builder.Append(OpenColDiv(sortHelper.GenerateColSizeAlgo(item, width),item));

                //Console.Write(((Button)con).Name);
                    switch (item.Type)
                    {
                        case ControlType.Button:
                            builder.Append(this.GetButton(item));
                            break;

                        case ControlType.CheckBox:
                            builder.Append(this.GetCheckBox(item));
                            break;

                        case ControlType.RadioButton:
                            builder.Append(this.GetRadio(item));
                            break;

                        case ControlType.ComboBox:
                            builder.Append(this.GetCombo(item));
                            break;

                        case ControlType.InputText:
                            builder.Append(this.GetText(item));
                            break;

                        case ControlType.InputPassword:
                            builder.Append(this.GetPassword(item));
                            break;

                        case ControlType.DatePicker:
                            builder.Append(this.GetDatePicker(item));
                            break;

                        case ControlType.Paragraph:
                            builder.Append(this.GetPara(item));
                            break;

                        case ControlType.Label:
                            builder.Append(this.GetLabel(item));
                            break;

                        case ControlType.TextArea:
                            builder.Append(this.GetTextArea(item));
                            break;

                        case ControlType.Image:
                            builder.Append(this.GetImage(item));
                            break;

                        case ControlType.HyperLink:
                            builder.Append(this.GetHyperLink(item));
                            break;

                        case ControlType.Iframe:
                            builder.Append(this.GetHyperLink(item));
                            break;

                        case ControlType.HLine:
                            builder.Append(this.GetHLine(item));
                            break;

                    }
                    //close col div
                    builder.Append(EndTag(div));
                }
                //close row div
                builder.Append(EndTag(div));
                builder.Append(newline);
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
            string endTag = string.Format(@"    </" + tagName + ">" + "\n");
            return endTag;
        }

        public string DynamicContent()
        {
            return string.Empty;
        }

        public string StartDiv(double left, double top)
        {
            string div = string.Format(@"  <div style='margin-left:{0}px;margin-top:{1}px' />" +newline, left, top);
            return div;
        }

        public string OpenRowDiv(double height,int marginTop)
        {
            string rowDiv = string.Format(@"    <div class='row' style='height: {0}px;margin-top: {1}px'>" +newline, height, marginTop);
            return rowDiv;
        }

        public string OpenColDiv(int colSize, Control item)
        {
            string value; 
            item.Styles.TryGetValue("margin-left", out value);
            string colDiv = string.Format(@"<div class='col-md-{0}' style='margin-left: {1}'>" + newline, colSize, value);
            return colDiv;
        }

        //public string OpenColDiv(int colSize)
        //{
            
        //    string colDiv = string.Format(@"      <div class='col-md-{0}' >" + newline, colSize);
        //    return colDiv;
        //}

        //public string OpenColDiv(Control item)
        //{
        //    double pageWidth = 900;
        //    double value = (item.Width / pageWidth)*12;
        //    int colSize = (int)(Math.Round(value));
        //    if (colSize < 2)
        //    {
        //        colSize = 2;
        //    }
        //    string colDiv = string.Format(@"<div class='col-md-{0}'>" + newline, colSize);

        //    return colDiv;
        //}


        public string InputTag(string inputType, string inputName, string inputValue)
        {
            return string.Empty;
        }

        public string GetButton(Control control)
        {
            Button button = control as Button;
            string btn;
            if (button != null && button.Value != null)
            {
                 btn = string.Format(@"    <input type='button' value='{0}'/>" + newline, button.Value);
            }
            else
            {
                btn = string.Format(@"    <input type='button'/>" + newline);
            }

            return btn;
        }

        public string GetCheckBox(Control checkbox)
        {
            string check = string.Format(@"          <input type='checkbox' style='width: 100%'/>" + newline);
           
            return check;
        }

        public string GetRadio(Control radio)
        {
            string radioBtn = string.Format(@"          <input type='radio' style='width: 100%'/>" + newline);
            return radioBtn;
        }

        public string GetCombo(Control combo)
        {
            string select = string.Format(
                            @"          <select>
        <option value='yourMom'>Mom</option>
        <option value='myMom'>My Mom</option>
   </select>" + newline);

            return select;
        }

        public string GetText(Control text)
        {
            string txt = string.Format(@"          <input type='text' style='width: 100%'/>" + newline);
            
            return txt;
        }

        public string GetPassword(Control password)
        {
            string pass = string.Format(@"          <input type='password' style='width: 100%'/>" + newline);
            return pass;
        }

        public string GetDatePicker(Control datepicker)
        {
            string date = string.Format(@"          <input type='date' style='width: 100%'/>",
                                          datepicker.X,
                                          datepicker.Y);
            return date;
        }

        public string GetPara(Control para) // for paragraph 
        {
            string paragraph = string.Empty;
            Paragraph content = para as Paragraph;
            if (content != null)
            {
                paragraph = string.Format(@"          <p style='width: 100%'>{0}</p>", content.Content);
            }
            else
            {
                paragraph = string.Format(@"          <p style='width: 100%'></p>");
            }
                return paragraph;
        }

        public string GetLabel(Control control)
        {
            string lbl = string.Empty;
            Label label = control as Label;

            if (label != null && label.Value != null)
            {
                lbl = string.Format(@"          <label style='width: 100%'>{0}</label>" + newline, label.Value);
            }

            lbl = string.Format(@"          <label style='width: 100%'>{0}</label>" + newline);

            return lbl;
        }

        public string GetTextArea(Control textarea)
        {
            return string.Empty;
        }

        public string GetImage(Control img)
        {
            string image = string.Format(@"          <img src='img.jpg' style='width: 100%' />" + newline);
            return image;
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
