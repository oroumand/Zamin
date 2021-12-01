using Zamin.Utilities.Services.Localizations;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;

namespace Zamin.Infra.Tools.Srlzr.EPPlus;
public static class TestClass
    {
        public static byte[] ToExcelByteArray<T>(this List<T> list, ITranslator resourceManager, string sheetName = "Result")
        {
            using ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);

            var t = typeof(T);
            var Headings = t.GetProperties();

            for (int i = 0; i < Headings.Count(); i++)
            {

                ws.Cells[1, i + 1].Value = resourceManager[Headings[i].Name];
            }

            //populate our Data
            if (list.Count() > 0)
            {
                ws.Cells["A2"].LoadFromCollection(list);
            }

            using (ExcelRange rng = ws.Cells["A1:BZ1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White);
            }


            var array = pck.GetAsByteArray();
            return array;
        }


        public static DataTable ToDataTableFromExcel(this byte[] bytes)
        {
            var pck = new ExcelPackage();
            using (MemoryStream ms = new MemoryStream(bytes))
                pck.Load(ms);
            var ws = pck.Workbook.Worksheets.First();
            DataTable tbl = new DataTable();
            bool hasHeader = true;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }
            var startRow = hasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var row = tbl.NewRow();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
                tbl.Rows.Add(row);
            }
            pck.Dispose();
            return tbl;
        }
    }
