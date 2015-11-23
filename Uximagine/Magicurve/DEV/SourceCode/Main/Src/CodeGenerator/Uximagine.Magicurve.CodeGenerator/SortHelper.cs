using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    public class SortHelper
    {
        List<Control> controls = new List<Control>();

        public List<Control> SortListYProperty(List<Control> list)
        {
            //sorting input list

            var query =
                from con in list
                orderby con.Y
                select con;

            foreach (var control in query)
            {
                controls.Add(control);
            }

            return controls;
        }

        public List<Row> DivAlgorithm(List<Control> list)
        {
            double defaultHeight = 40;
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
            listOfList.Add(new Row()
            {
                Controls = new List<Control>()
                    { 
                        list[0] 
                    },
                RowIndex = 0,
                Height = maxHeight
            });

            for (int i = 1; i <= (list.Count) - 1; i++)
            {
                int previousY = list[i - 1].Y;
                int currentY = list[i].Y;

                if (currentY > previousY + maxHeight)
                {
                    if (list[i].Height > defaultHeight)
                    {
                        maxHeight = list[i].Height;
                    }
                    else
                    {
                        maxHeight = defaultHeight;
                    }
                    
                    listOfList.Add(new Row()
                    {
                        Controls = new List<Control>()
                        {
                            list[i] 
                        },
                        RowIndex = ++rowIndex,
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

        public List<Row> SortListXProperty(List<Row> rowList)
        {
            List<Row> finalizeRowList = new List<Row>();


            foreach (Row row in rowList)
            {

                List<Control> finalizeControlList = new List<Control>();
                var query =
                from con in row.Controls
                orderby con.X
                select con;

                finalizeControlList = query.ToList();

                finalizeRowList.Add(new Row()
                {
                    Controls = finalizeControlList,
                    Height = row.Height,
                    RowIndex = row.RowIndex
                });
            }


            return finalizeRowList;
        }

        public int GenerateColSizeAlgo(Control item, double pageWidth)
        {
            double value = (item.Width / pageWidth) * 12;
            int colSize = (int)(Math.Round(value));
            if (colSize < 2)
            {
                colSize = 2;
            }
            return colSize;
        }


    }
}
