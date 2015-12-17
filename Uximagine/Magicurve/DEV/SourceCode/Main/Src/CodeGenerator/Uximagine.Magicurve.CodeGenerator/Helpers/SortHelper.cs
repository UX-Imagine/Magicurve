namespace Uximagine.Magicurve.CodeGenerator.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Uximagine.Magicurve.CodeGenerator.Common;
    using Uximagine.Magicurve.Core.Models;
    using Uximagine.Magicurve.Core.Shapes;

    /// <summary>
    /// The sort helper.
    /// </summary>
    public static class SortHelper
    {
        public static double PreviousMapWidth { get; set; }

        /// <summary>
        /// The sort by y.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Control> SortByY(this List<Control> list)
        {
          return list.OrderBy(c => c.Y).ToList();
        }

        /// <summary>
        /// The generate division.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Row> GenerateDivisions(this List<Control> list)
        {
            double defaultHeight = ConfigurationData.DefaultRowHeight;
            double maxHeight;
            List<Row> listOfList = new List<Row>();
            Control minYControl = list[0];
            if (list[0].Height < defaultHeight)
            {
                maxHeight = defaultHeight;
            }
            else
            {
                maxHeight = list[0].Height;
            }
             
            int rowIndex = 0;
            listOfList.Add(new Row
            {
                Controls = new List<Control>
                    { 
                        list[0] 
                    },
                RowIndex = 0,
                TopMargin = list[0].Y - 10,
                Height = maxHeight
            });

            for (int i = 1; i <= list.Count - 1; i++)
            {
                int previousY = list[i - 1].Y;
                int currentY = list[i].Y;

                //// if (currentY > previousY + maxHeight)
                if (currentY > previousY + defaultHeight)
                {
                    maxHeight = list[i].Height > defaultHeight ? list[i].Height : defaultHeight;

                    listOfList.Add(new Row
                    {
                        Controls = new List<Control>
                        {
                            list[i] 
                        },
                        RowIndex = ++rowIndex,
                        //// TopMargin = list[i].Y-(list[i-1].Y+(int)list[i-1].Height),
                        TopMargin = list[i].Y - (list[i - 1].Y + (int)defaultHeight),
                        Height = maxHeight
                    });
                }
                else
                {

                    if (list[i].Height > maxHeight)
                    {
                        maxHeight = list[i].Height;
                    }
                    listOfList[rowIndex].Controls.Add(list[i]);
                    listOfList[rowIndex].Height = maxHeight;
                }
            }

            return listOfList;
        }

        /// <summary>
        /// The sort list x property.
        /// </summary>
        /// <param name="rowList">
        /// The row list.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Row> SortListXProperty(this List<Row> rowList)
        {
            List<Row> finalizeRowList = new List<Row>();

            foreach (Row row in rowList)
            {
                var query =
                from con in row.Controls
                orderby con.X
                select con;

                List<Control> finalizeControlList = query.ToList();

                if (finalizeControlList[0].Styles == null)
                {
                    finalizeControlList[0].Styles = new List<Style>()
                                                    {
                                                        new Style()
                                                            {
                                                                Key = "col-md-offset",
                                                                Value = finalizeControlList[0].X.ToString()
                                                            }
                                                    };
                }
                else
                {
                    finalizeControlList[0].Styles.Add(new Style("col-md-offset", finalizeControlList[0].X.ToString()));
                }
                

                for (int i = 1; i < finalizeControlList.Count; i++)
                {
                    if (finalizeControlList[i].Styles == null)
                    {
                        finalizeControlList[i].Styles = new List<Style>();
                    }
                    
                    int x = finalizeControlList[i].X - finalizeControlList[i - 1].X;

                    finalizeControlList[i].Styles.Add(new Style("col-md-offset", x.ToString()));

                }

                    finalizeRowList.Add(new Row()
                {
                    Controls = finalizeControlList,
                    Height = row.Height,
                        RowIndex = row.RowIndex,
                        TopMargin = row.TopMargin
                });
            }


            return finalizeRowList;
        }

        /// <summary>
        /// The get column size.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetColumnSize(this Control item)
        {
            double value = (item.Width / ConfigurationData.DefaultPageWidth) * 12;
            int colSize = (int)(Math.Round(value));
            if (colSize <= 1)
            {
                colSize = 2;
            }

            double mappedWidth = (colSize / 12.0) * ConfigurationData.DefaultPageWidth;

            int offset = int.Parse(item.Styles.First(s => s.Key == "col-md-offset").Value);
            double actualLeft = (offset - PreviousMapWidth) / ConfigurationData.DefaultPageWidth * 12;
            int roundOffset = (int)(Math.Round(actualLeft));
            item.Styles.First(s => s.Key == "col-md-offset").Value = roundOffset.ToString();
            PreviousMapWidth = mappedWidth;
            return colSize;
        }

    }
}
