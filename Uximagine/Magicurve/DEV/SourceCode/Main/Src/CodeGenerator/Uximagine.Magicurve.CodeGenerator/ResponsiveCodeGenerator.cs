using System.Collections.Generic;
using System.Text;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.CodeGenerator.Common;
using Uximagine.Magicurve.CodeGenerator.Helpers;
using System;

namespace Uximagine.Magicurve.CodeGenerator
{
    /// <summary>
    /// The responsive code generator.
    /// </summary>
    public class ResponsiveCodeGenerator : IGenerator
    {
        /// <summary>
        /// The create HTML code.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CreateHtmlCode(List<Control> controls, double width, double height)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.NormalStartTag(CommonTags.HtmlTags.Html));
            builder.Append(this.NormalStartTag(CommonTags.HtmlTags.Head));
            builder.Append(this.ApplyCss(CommonTags.HtmlTags.BootstrapCss));
            builder.Append(this.EndTag(CommonTags.HtmlTags.Head));
            builder.Append(this.GetBody());

            controls.ForEach(
                c =>
                {
                    c.Width = c.Width / width * ConfigurationData.DefaultPageWidth;
                    c.Height = c.Height / height * ConfigurationData.DefaultPageHeight;
                    c.X = (int)(c.X / width * ConfigurationData.DefaultPageWidth);
                    c.Y = (int)(c.Y / height * ConfigurationData.DefaultPageHeight);
                });

            List<Row> sortedControls = controls.SortByY().GenerateDivisions();
            List<Row> finalRowListControls = sortedControls.SortListXProperty();

            //// execute sorted list and check control types
            foreach (Row row in finalRowListControls)
            {
                builder.Append(this.OpenRowDiv(row.Height, row.TopMargin));
                SortHelper.PreviousMapWidth = 0;
                foreach (Control item in row.Controls)
                {
                    builder.Append(this.OpenColDiv(item.GetColumnSize(), item));

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
                            builder.Append(this.GetIframe(item));
                            break;

                        case ControlType.HLine:
                            builder.Append(this.GetHLine(item));
                            break;

                    }

                    //// close col div
                    builder.Append(this.EndTag(CommonTags.HtmlTags.Div));
                }

                //// close row div
                builder.Append(this.EndTag(CommonTags.HtmlTags.Div));
                builder.Append(Environment.NewLine);
            }

            builder.Append(this.ApplyScript(CommonTags.HtmlTags.Jquery));
            builder.Append(this.ApplyScript(CommonTags.HtmlTags.BootstrapJs));
            builder.Append(this.GetFooter());

            return builder.ToString(); //// return generated HTML code as string
        }

        /// <summary>
        /// The get footer.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetFooter()
        {
            string footer = string.Format(@"</div> </body>" + Environment.NewLine + "</html>");
            return footer;
        }
        
        /// <summary>
        /// The get body.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetBody()
        {
            string body = $"<body>{Environment.NewLine}<div class='container' style='margin-top:{ConfigurationData.DefaultMarginTop}px;'>{Environment.NewLine}";
            return body;
        }

        public string ApplyCss(string src)
        {
            string content = $"<link rel='stylesheet' type='text/css' href='{src}'/> {Environment.NewLine}";
            return content;
        }

        public string ApplyScript(string src)
        {
            string script =$"<script src='{src}'></script> {Environment.NewLine}";
            return script;
        }

        public string NormalStartTag(string tagName)
        {
            string startTag = $"<{tagName}>{Environment.NewLine}";
            return startTag;
        }

        public string EndTag(string tagName)
        {
            string endTag = $"</{tagName}>{Environment.NewLine}";
            return endTag;
        }

        public string DynamicContent()
        {
            return string.Empty;
        }

        public string StartDiv(double left, double top)
        {
            string div =$"<div style='margin-left:{left}px;margin-top:{top}px'>";
            return div;
        }

        public string OpenRowDiv(double height,int marginTop)
        {
            string rowDiv = $"<div class='row' style='margin-top:{marginTop}px'>{Environment.NewLine}";
            return rowDiv;
        }

        public string OpenColDiv(int colSize, Control item)
        {
            int value; 
            item.Styles.TryGetValue("col-md-offset", out value);

            string colDiv = $"<div class='col-md-{colSize} col-md-offset-{value}'>";
            ////string colDiv = $"<div class='col-md-{colSize}'>";
            return colDiv;
        }

        /// <summary>
        /// Inputs the tag.
        /// </summary>
        /// <param name="inputType">Type of the input.</param>
        /// <param name="inputName">Name of the input.</param>
        /// <param name="inputValue">The input value.</param>
        /// <returns>
        /// The input tag.
        /// </returns>
        public string InputTag(string inputType, string inputName, string inputValue)
        {
            return $"<input type='{inputType}' name='{inputName}' value='{inputValue}'>{inputValue}</input>";
        }

        public string GetButton(Control control)
        {
            Button button = control as Button;
            string btn;
            if (button?.Value != null)
            {
                 btn = $"<input type='button' class='btn btn-default' value='{button.Value}'/>{Environment.NewLine}";
            }
            else
            {
                btn = $"<input type='button' class='btn btn-default' value='Button'/>{Environment.NewLine}";
            }

            return btn;
        }

        /// <summary>
        /// Gets the CheckBox.
        /// </summary>
        /// <param name="checkbox">The check box.</param>
        /// <returns>
        /// The check box code.
        /// </returns>
        public string GetCheckBox(Control checkbox)
        {
            string check = $"<div style='width: 100%'><input type='checkbox'/>Check this.</div>{Environment.NewLine}";
            return check;
        }

        /// <summary>
        /// Gets the radio.
        /// </summary>
        /// <param name="radio">
        /// The radio.
        /// </param>
        /// <returns>
        /// The radio code.
        /// </returns>
        public string GetRadio(Control radio)
        {
            string radioBtn = $"<div style='width: 100%'><input type='radio'> Radio</input></div>{Environment.NewLine}";
            return radioBtn;
        }

        /// <summary>
        /// Gets the combo.
        /// </summary>
        /// <param name="combo">
        /// The combo.
        /// </param>
        /// <returns>
        /// The combo code.
        /// </returns>
        public string GetCombo(Control combo)
        {
            string select =
                            $@"<select>
                                    <option value='value'>Select</option>
                                    <option value='value1'>Value1</option>
                               </select>";
            return select;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The text code.
        /// </returns>
        public string GetText(Control text)
        {
            string txt = $"<input type='text' class='form-control' style='width: 100%' placeholder='text input'/>{Environment.NewLine}";

            return txt;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The password text.
        /// </returns>
        public string GetPassword(Control password)
        {
            string pass = $"<input type='password' class='form-control' style='width: 100%' placeholder='password'/>{Environment.NewLine}";
            return pass;
        }

        /// <summary>
        /// Gets the date picker.
        /// </summary>
        /// <param name="datepicker">
        /// The date picker.
        /// </param>
        /// <returns>
        /// The data picker code.
        /// </returns>
        public string GetDatePicker(Control datepicker)
        {
            string date = $"<input type='date' class='form-control' style='width: 100%'/>";
            return date;
        }

        /// <summary>
        /// Gets the para.
        /// </summary>
        /// <param name="para">
        /// The para.
        /// </param>
        /// <returns>
        /// The paragraph code.
        /// </returns>
        public string GetPara(Control para) // for paragraph 
        {
            string paragraph;

            Paragraph content = para as Paragraph;

            if (content != null)
            {
                paragraph = $@"<p style='width: 100%'>{content.Content}</p>";
            }
            else
            {
                paragraph = $@"<p style='width: 100%'>This is a sample paragraph. use the interactive tool to edit the content.</p>";
            }
            return paragraph;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The label code.
        /// </returns>
        public string GetLabel(Control control)
        {
            string lbl;
            Label label = control as Label;

            if (label?.Value != null)
            {
                lbl = $@"<label class='control-label' style='width: 100%'>{label.Value}</label> {Environment.NewLine}";
            }
            else
            {
                lbl = $@"<label class='control-label' style='width: 100%'>Label Value</label> {Environment.NewLine}";
            }

            return lbl;
        }

        /// <summary>
        /// Gets the text area.
        /// </summary>
        /// <param name="textarea">
        /// The text area.
        /// </param>
        /// <returns>
        /// The text area code.
        /// </returns>
        public string GetTextArea(Control textarea)
        {
            return $"<textarea style='width: 100%'>The text area.</textarea>";
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="img">The image.</param>
        /// <returns>
        /// The image code.
        /// </returns>
        public string GetImage(Control img)
        {
            string image = $"<img src='#' class='img-responsive' style='max-width: 90%' />{Environment.NewLine}";
            return image;
        }

        /// <summary>
        /// Gets the hyper link.
        /// </summary>
        /// <param name="hyperlink">The hyper link.</param>
        /// <returns>
        /// The hyper link code.
        /// </returns>
        public string GetHyperLink(Control hyperlink)
        {
            return $"<a href='#'>The Link</a>{Environment.NewLine}";
        }

        /// <summary>
        /// Gets the i frame.
        /// </summary>
        /// <param name="iframe">The i frame.</param>
        /// <returns>
        /// The i frame.
        /// </returns>
        public string GetIframe(Control iframe)
        {
            return $"<div class='embed-responsive embed-responsive-4by3'>{Environment.NewLine}<iframe src='#' class='embed-responsive-item' style='width:100%'></iframe>{Environment.NewLine}</div>{Environment.NewLine}";
        }

        /// <summary>
        /// Gets the h line.
        /// </summary>
        /// <param name="hline">
        /// The horizontal line.
        /// </param>
        /// <returns>
        /// The horizontal line code.
        /// </returns>
        public string GetHLine(Control hline)
        {
            return $"<div style='width: 100%'><hr></div>";
        }

    }
    
}
