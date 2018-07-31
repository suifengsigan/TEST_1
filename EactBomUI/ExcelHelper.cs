using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EactBom
{
    public class ExcelHelper
    {
        public static void ExportExcelBom(List<ElecManage.Electrode> cuprums, ElecManage.MouldInfo mouldInfo)
        {
            ExportPQExcelBom(cuprums, mouldInfo);
        }
        /// <summary>
        /// 导物料单
        /// </summary>
        private static void ExportPQExcelBom(List<ElecManage.Electrode> cuprums,ElecManage.MouldInfo mouldInfo)
        {
            if (cuprums.Count == 0) return;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("Bom", "EACT_BOM_TEMPLATE_PQ.xls"));
            var newDir = Path.Combine(Path.GetDirectoryName(Snap.Globals.WorkPart.FullPath), "BOM");
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }
            var newPath = Path.Combine(newDir, Path.GetFileNameWithoutExtension(Snap.Globals.WorkPart.FullPath)+"-BOM.xls");
            using (FileStream fs = new FileStream(path, FileMode.Open))//path=mmm.xls;
            {
                IWorkbook workbook = new HSSFWorkbook(fs);//创建Workbook
                ISheet sheet = workbook.GetSheetAt(0);//获取sheet

                IFont titleFont = workbook.CreateFont();
                titleFont.FontHeightInPoints = 10;//设置字体大小
                titleFont.FontName = "宋体";

                ICellStyle styleThin = workbook.CreateCellStyle();
                styleThin.BorderBottom = BorderStyle.Thin;
                styleThin.BorderLeft = BorderStyle.Thin;
                styleThin.BorderRight = BorderStyle.Thin;
                styleThin.BorderTop = BorderStyle.Thin;
                styleThin.SetFont(titleFont);

                ICellStyle styleBottom = workbook.CreateCellStyle();
                styleBottom.BorderBottom = BorderStyle.Medium;
                styleBottom.BorderLeft = BorderStyle.Thin;
                styleBottom.BorderRight = BorderStyle.Thin;
                styleBottom.BorderTop = BorderStyle.Thin;
                styleBottom.SetFont(titleFont);

                ICellStyle styleRight = workbook.CreateCellStyle();
                styleRight.BorderBottom = BorderStyle.Thin;
                styleRight.BorderLeft = BorderStyle.Thin;
                styleRight.BorderRight = BorderStyle.Medium;
                styleRight.BorderTop = BorderStyle.Thin;

                ICellStyle styleRightBottom = workbook.CreateCellStyle();
                styleRightBottom.BorderBottom = BorderStyle.Medium;
                styleRightBottom.BorderLeft = BorderStyle.Thin;
                styleRightBottom.BorderRight = BorderStyle.Medium;
                styleRightBottom.BorderTop = BorderStyle.Thin;
                styleRightBottom.SetFont(titleFont);

                //模号 件号
                sheet.GetRow(2).GetCell(2).SetCellValue(mouldInfo.MODEL_NUMBER);
                sheet.GetRow(2).GetCell(6).SetCellValue(mouldInfo.MR_NUMBER);
                sheet.GetRow(2).GetCell(11).SetCellValue(EactBomBusiness.Instance.ConfigData.DataBaseInfo.LoginUser);
                sheet.GetRow(2).GetCell(18).SetCellValue(DateTime.Now.ToString());
                sheet.GetRow(2).GetCell(18).CellStyle = styleRight;

                //电极列表
                int rowStart = 4;

                int sum = 0;

                foreach (var item in cuprums)
                {
                    var style = styleThin;
                    if (cuprums.IndexOf(item) == cuprums.Count - 1)
                    {
                        style = styleBottom;
                    }
                    var partName = EactBomBusiness.Instance.GetPARTFILENAME(item.ElecBody, mouldInfo);
                    var info = item.GetElectrodeInfo();

                    int cellIndex = 0;
                    sheet.CreateRow(rowStart).CreateCell(cellIndex).SetCellValue(rowStart-3);//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(partName);//获取第一行/创建第二单元格/设置第二单元格的内容
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //开料尺寸
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.ElecCuttingSize(EactBomBusiness.Instance.ConfigData.PQBlankStock).Replace('x', '*'));
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //夹具
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.ELEC_CLAMP_GENERAL_TYPE);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //数量
                    cellIndex++;
                    var tempV = info.FINISH_NUMBER + info.MIDDLE_NUMBER + info.ROUGH_NUMBER;
                    sum += tempV;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(tempV);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //实际尺寸
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.ElecSize.Replace('x', '*'));
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //粗公间隙
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.ROUGH_SPACE);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //中公间隙
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.MIDDLE_SPACE);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //精公间隙
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.FINISH_SPACE);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //粗公放电纹
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.R_SMOOTH);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //中公放电纹
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.M_SMOOTH);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //精公放电纹
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.F_SMOOTH);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //粗公数量
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.ROUGH_NUMBER);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //中公数量
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.MIDDLE_NUMBER);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //精公数量
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.FINISH_NUMBER);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //粗公材质
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.MAT_NAME);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //中公材质
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.MAT_NAME);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //精公材质
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue(info.MAT_NAME);
                    sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = style;

                    //备注
                    cellIndex++;
                    sheet.GetRow(rowStart).CreateCell(cellIndex).SetCellValue("");
                    if (cuprums.IndexOf(item) == cuprums.Count - 1)
                    {
                        sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = styleRightBottom;
                    }
                    else
                    {
                        sheet.GetRow(rowStart).GetCell(cellIndex).CellStyle = styleRight;
                    }

                    ++rowStart;
                }

                //Todo 数量总和
                sheet.CreateRow(rowStart).CreateCell(4).SetCellValue(sum);//创建第一行/创建第一单元格/设置第一单元格的内容[可以分开创建，但必须先创建行才能创建单元格不然报错]

                using (FileStream fsW = File.Create(newPath))//path=mmm.xls;
                {
                    workbook.Write(fsW);//保存文件
                }
            }
        }
    }
}
