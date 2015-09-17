namespace Uximagine.Magicurve.Services.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinqTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //1.Obtain the data source
            int[] array = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] arr = new int[]{2,4,6,8,10};
            //2.Create the query
            var query =
                from num in array
                where (num % 2) == 0
                select num;

            int i = 0;
            //3.Execute the query
            foreach (int num in query)
            {
                while (i<5)
                {
                    Assert.AreEqual(num, arr[i]);
                    i++;
                    break;
                }
                Console.Out.Write("{0,1}", num);
                
                
            }
            //end of basic operation in linq
            Console.Write("\n");


            //sorting using linq "orderby"
            string[] name = new string[] { "Dilsha", "Binu", "Chan", "Akesh" };

            var sortQuery =
                from stu in name
                orderby stu ascending
                select stu;

            

            foreach (string so in sortQuery)
            {
                Console.Write(so+"\n");
                
            }
           //end of sorting




            
            
        }
    }
}
