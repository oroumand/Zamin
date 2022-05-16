using Aspose.Cells;
using System.Data;
using System.Drawing;

namespace Zamin.Extensions.Serializers.Asposes.Extensions;

public static class ExcelExtentions
{
    public static byte[] ToExcelByteArray<T>(this List<T> list, string sheetName = "Result")
    {
        Workbook workbook = new Workbook();

        Worksheet ws = workbook.Worksheets.Add(sheetName);

        var t = typeof(T);
        var headings = t.GetProperties();


        if (list.Count() > 0)
        {
            ws.Cells.ImportCustomObjects(list, headings.Select(c => c.Name).ToArray(), true, 0, 0, list.Count, true, "dd/mm/yyyy", false);
        }

        ws.Cells.Style.Font.IsBold = true;
        ws.Cells.Style.Borders.SetStyle(CellBorderType.Thin);
        ws.Cells.Style.BackgroundColor = Color.FromArgb(79, 129, 189);
        ws.Cells.Style.Font.Color = Color.White;


        var array = workbook.SaveToStream().ToArray();

        return array;
    }

    //public static DataTable ToDataTableFromExcel(this byte[] bytes)
    //{
    //    var excelPackage = new ExcelPackage();

    //    using (MemoryStream memoryStream = new MemoryStream(bytes))
    //        excelPackage.Load(memoryStream);

    //    var ws = excelPackage.Workbook.Worksheets.First();

    //    DataTable dataTable = new();

    //    bool hasHeader = true;

    //    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
    //    {
    //        dataTable.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
    //    }

    //    var startRow = hasHeader ? 2 : 1;

    //    for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
    //    {
    //        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
    //        var row = dataTable.NewRow();
    //        foreach (var cell in wsRow)
    //        {
    //            row[cell.Start.Column - 1] = cell.Text;
    //        }
    //        dataTable.Rows.Add(row);
    //    }

    //    excelPackage.Dispose();

    //    return dataTable;
    //}
}
