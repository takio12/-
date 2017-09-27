using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            IWorkbook book = new XSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1!!");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue("Hellosss");

            ISheet sheet2 = book.CreateSheet();
            for(int i=0; i < 11; i++)
            {
                IRow row = sheet2.CreateRow(i);
                for(int j = 0; j < 11; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(j);
                }
                
            }
            using (FileStream fs = File.Create(@"D:\Sample01.xls"))
            {
                book.Write(fs);
            }


        }
    }
}
