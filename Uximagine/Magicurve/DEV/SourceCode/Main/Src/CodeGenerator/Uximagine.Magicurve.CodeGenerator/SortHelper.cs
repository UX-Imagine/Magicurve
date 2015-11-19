using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                orderby con.X 
                select con;

            foreach (var con in query)
            {
                controls.Add(con);
            }

            return controls;
        }

        public List<Row> DivAlgorithm(List<Control> list)
        {
            List<Row> listOfList = new List<Row>();
            Control minYControl = list[0];
            double maxHeight = list[0].Height;
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
                    maxHeight = list[i].Height;
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

        //public List<Row> SortListXProperty(List<Row> rowList)
        //{


        //    var query =
        //        from con in rowList
        //        orderby con.Controls.
        //        select con;

        //    return rowList;
        //}


    }
}
