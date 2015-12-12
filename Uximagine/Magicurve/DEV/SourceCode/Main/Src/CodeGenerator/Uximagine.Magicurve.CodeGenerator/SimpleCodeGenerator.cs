using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    using Uximagine.Magicurve.CodeGenerator.Common;

    /// <summary>
    /// code generator class
    /// </summary>
    public class SimpleCodeGenerator : IGenerator
    {

        /// <summary>
        /// Gets or sets the custom CSS.
        /// </summary>
        /// <value>
        /// The custom CSS.
        /// </value>
        public string CustomCss { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCodeGenerator"/> class.
        /// </summary>
        public SimpleCodeGenerator()
        { 
        }

        /// <summary>
        /// Creates the HTML code.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// The HTML code.
        /// </returns>
        public string CreateHtmlCode(List<Control> controls, double width, double height)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.NormalStartTag(CommonTags.HtmlTags.Html));
            builder.Append(this.NormalStartTag(CommonTags.HtmlTags.Head));
           
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

            //// sorting input list
            var query =
                from con in controls
                orderby con.Y
                select con;

            //// execute sorted list and check control types
            foreach (Control con in query)
            {
                switch (con.Type)
                {
                    case ControlType.Button:
                        builder.Append(this.GetButton(con));
                        break;

                    case ControlType.CheckBox:
                        builder.Append(this.GetCheckBox(con));
                        break;

                    case ControlType.RadioButton:
                        builder.Append(this.GetRadio(con));
                        break;

                    case ControlType.ComboBox:
                        builder.Append(this.GetCombo(con));
                        break;

                    case ControlType.InputText:
                        builder.Append(this.GetText(con));
                        break;

                    case ControlType.InputPassword:
                        builder.Append(this.GetPassword(con));
                        break;

                    case ControlType.DatePicker:
                        builder.Append(this.GetDatePicker(con));
                        break;

                    case ControlType.Paragraph:
                        builder.Append(this.GetPara(con));
                        break;

                    case ControlType.Label:
                        builder.Append(this.GetLabel(con));
                        break;

                    case ControlType.TextArea:
                        builder.Append(this.GetTextArea(con));
                        break;

                    case ControlType.Image:
                        builder.Append(this.GetImage(con));
                        break;

                    case ControlType.HyperLink:
                        builder.Append(this.GetHyperLink(con));
                        break;

                    case ControlType.Iframe:
                        builder.Append(this.GetIframe(con));
                        break;

                    case ControlType.HLine:
                        builder.Append(this.GetHLine(con));
                        break;

                    case ControlType.Range:
                        builder.Append(this.GetRange(con));
                        break;
                }
            }

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
            string footer = $@"</body> {Environment.NewLine} </Html>";
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
            string body = string.Format(@"<body width='{0}' height='{1}'>" + Environment.NewLine + "<div>" + Environment.NewLine, 
                ConfigurationData.DefaultPageWidth, 
                ConfigurationData.DefaultPageHeight);
            return body;
        }

        /// <summary>
        /// The apply CSS.
        /// </summary>
        /// <param name="src">
        /// The SRC.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ApplyCss(string src)
        {
            return string.Empty;
        }

        /// <summary>
        /// The apply script.
        /// </summary>
        /// <param name="src">
        /// The SRC.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ApplyScript(string src)
        {
            return string.Empty;
        }

        /// <summary>
        /// The normal start tag.
        /// </summary>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string NormalStartTag(string tagName)
        {
            string startTag = "<" + tagName + ">" + Environment.NewLine;
            return startTag;
        }

        /// <summary>
        /// The end tag.
        /// </summary>
        /// <param name="tagName">
        /// The tag name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string EndTag(string tagName)
        {
            string endTag = "</" + tagName + ">" + Environment.NewLine;
            return endTag;
        }

        /// <summary>
        /// The dynamic content.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DynamicContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// The start div.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string StartDiv(double left, double top)
        {
            string div = string.Format(@" <div style='margin-left:{0}px;margin-top:{1}px' />" + Environment.NewLine, left, top);
            return div;
        }

        /// <summary>
        /// The input tag.
        /// </summary>
        /// <param name="inputType">
        /// The input type.
        /// </param>
        /// <param name="inputName">
        /// The input name.
        /// </param>
        /// <param name="inputValue">
        /// The input value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string InputTag(string inputType, string inputName, string inputValue)
        {
            return $"<input type='{inputType}' name='{inputName}' value='{inputValue}'/>";
        }

        /// <summary>
        /// The get button.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetButton(Control control)
        {
            Button button = control as Button;

            string btn =
                $"<input type='button' value='{button?.Value} Click Me' style='left:{control.X}px;top:{control.Y}px;position:absolute;'/>{Environment.NewLine}";

            return btn;
        }

        /// <summary>
        /// Gets the CheckBox.
        /// </summary>
        /// <param name="checkbox">
        /// The check box.</param>
        /// <returns>
        /// The check box syntax.
        /// </returns>
        public string GetCheckBox(Control checkbox)
        {
            string check = string.Format(
                @"    <input type='checkbox' style='left:{0}px;top:{1}px;position:absolute'/>" + Environment.NewLine,
                checkbox.X,
                checkbox.Y);

            return check;
        }

        /// <summary>
        /// Gets the radio.
        /// </summary>
        /// <param name="radio">
        /// The radio.
        /// </param>
        /// <returns>
        /// The radio button.
        /// </returns>
        public string GetRadio(Control radio)
        {
            return $"<div style='left:{radio.X}px;top:{radio.Y}px;position:absolute'><input type='radio' value='radio' > Radio</input></div>";
        }

        /// <summary>
        /// Gets the combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <returns>
        /// The combo syntax.
        /// </returns>
        public string GetCombo(Control combo)
        {
            string select = string.Format(
                            @"    <select id='styledSelect' class='blueText' style='left:{0}px;top:{1}px;position:absolute'>
        <option value='val1'>Select</option>
        <option value='val2'>Option1</option>
   </select>" + Environment.NewLine, 
                            combo.X, 
                            combo.Y);
            return select;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The text syntax.
        /// </returns>
        public string GetText(Control text)
        {
            string txt =
                $"<input type='text' style='left:{text.X}px;top:{text.Y}px;position:absolute' placeholder='text input'/> {Environment.NewLine}";
                                           
            return txt;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The password.
        /// </returns>
        public string GetPassword(Control password)
        {
            string pass = $@"    <input type='password' style='left:{password.X}px;top:{password.Y}px;position:absolute;'  placeholder='password input'/>";
            return pass;
        }

        public string GetRange(Control password)
        {
            string pass = $@"    <input type='range' style='left:{password.X}px;top:{password.Y}px;position:absolute;'/>";
            return pass;
        }

        /// <summary>
        /// Gets the date picker.
        /// </summary>
        /// <param name="datepicker">
        /// The date picker.
        /// </param>
        /// <returns>
        /// The syntax.
        /// </returns>
        public string GetDatePicker(Control datepicker)
        {
            string date = $@"    <input type='date' style='left:{datepicker.X}px;top:{datepicker.Y}px;position:absolute;'/>";
            return date;
        }

        /// <summary>
        /// Gets the para.
        /// </summary>
        /// <param name="para">The para.</param>
        /// <returns>
        /// The paragraph.
        /// </returns>
        public string GetPara(Control para) // for paragraph 
        {
            string paragraph = $@"    <p style='left:{para.X}px;top:{para.Y}px;position:absolute;'>This is a paragraph.<br/><br/><br/></p>";
            return paragraph;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// The label.
        /// </returns>
        public string GetLabel(Control label)
        {
            return $"<label style='left:{label.X}px;top:{label.Y}px;position:absolute;'> This is a label.</label>";
        }

        /// <summary>
        /// Gets the text area.
        /// </summary>
        /// <param name="textarea">The text area.</param>
        /// <returns>
        /// This will return HTML syntax for text area.
        /// </returns>
        public string GetTextArea(Control textarea)
        {
            return "<textarea style='left:{label.X}px;top:{label.Y}px;position:absolute;' rows='4' cols='50'>This is a text area.</textarea>";
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="img">The image.</param>
        /// <returns>
        /// The syntax.
        /// </returns>
        public string GetImage(Control img)
        {
            return $"<img src='' style='left:{img.X}px;top:{img.Y}px;position:absolute;width:{img.Width}px; height:{img.Height}px;'/>";
        }

        /// <summary>
        /// Gets the hyper link.
        /// </summary>
        /// <param name="hyperlink">The hyper link.</param>
        /// <returns>
        /// The syntax.
        /// </returns>
        public string GetHyperLink(Control hyperlink)
        {
            return $"<a href='#' style='left:{hyperlink.X}px;top:{hyperlink.Y}px;position:absolute;'>Go To Link</a>";
        }

        /// <summary>
        /// Gets the i frame.
        /// </summary>
        /// <param name="iframe">The i frame.</param>
        /// <returns>
        /// The I frame syntax.
        /// </returns>
        public string GetIframe(Control iframe)
        {
            return $"<iframe src='#' style='left:{iframe.X}px;top:{iframe.Y}px;position:absolute;width:{iframe.Width}px;height:{iframe.Height}px;'></iframe>";
        }

        /// <summary>
        /// Gets the h line.
        /// </summary>
        /// <param name="hline">The horizontal line.</param>
        /// <returns>
        /// The horizontal line HTML syntax.
        /// </returns>
        public string GetHLine(Control hline)
        {
            return $"<div style='left:{hline.X}px;top:{hline.Y}px;position:absolute;'> <hr></div>";
        }
    }
}
