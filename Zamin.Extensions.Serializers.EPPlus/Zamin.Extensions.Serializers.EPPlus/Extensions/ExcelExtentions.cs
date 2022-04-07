using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.EPPlus.Extensions;

public static class ExcelExtentions
{
    public static byte[] ToExcelByteArray<T>(this List<T> list, ITranslator translator, string sheetName = "Result")
    {
        using ExcelPackage excelPackage = new ExcelPackage();

        ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add(sheetName);

        var t = typeof(T);
        var headings = t.GetProperties();

        for (int i = 0; i < headings.Count(); i++)
        {

            ws.Cells[1, i + 1].Value = translator[headings[i].Name];
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

        var array = excelPackage.GetAsByteArray();

        return array;
    }

    public static DataTable ToDataTableFromExcel(this byte[] bytes)
    {
        var excelPackage = new ExcelPackage();

        using (MemoryStream memoryStream = new MemoryStream(bytes))
            excelPackage.Load(memoryStream);

        var ws = excelPackage.Workbook.Worksheets.First();

        DataTable dataTable = new();

        bool hasHeader = true;

        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
            dataTable.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        }

        var startRow = hasHeader ? 2 : 1;

        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
            var row = dataTable.NewRow();
            foreach (var cell in wsRow)
            {
                row[cell.Start.Column - 1] = cell.Text;
            }
            dataTable.Rows.Add(row);
        }

        excelPackage.Dispose();

        return dataTable;
    }
}
