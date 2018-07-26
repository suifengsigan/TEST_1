using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tool
{
    public class ExcelHelper
    {
        public static void ExcelTest(string path)
        {

            var newPath = Path.Combine(Path.GetDirectoryName(path), "BOM.xls");
            using (FileStream fs = new FileStream(path, FileMode.Open))//path=mmm.xls;
            {
                IWorkbook workbook = new HSSFWorkbook(fs);//创建Workbook
                ISheet sheet = workbook.GetSheetAt(0);//获取sheet
                int rowStart = 4;
                sheet.CreateRow(rowStart).CreateCell(0).SetCellValue("nami");//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]
                sheet.GetRow(rowStart).CreateCell(1).SetCellValue("robin");//获取第一行/创建第二单元格/设置第二单元格的内容


                ICellStyle style = workbook.CreateCellStyle();
                style.BorderBottom = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;
                sheet.GetRow(rowStart).GetCell(0).CellStyle = style;
                sheet.GetRow(rowStart).GetCell(1).CellStyle = style;

                sheet.GetRow(1).GetCell(1).CellStyle = style;
                sheet.GetRow(1).RowStyle = style;

                using (FileStream fsW = File.Create(newPath))//path=mmm.xls;
                {
                    workbook.Write(fsW);//保存文件
                }

            }
        }
    }
}
