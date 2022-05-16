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

    public static DataTable ToDataTableFromExcel(this byte[] bytes)
    {
        Workbook workbook = new Workbook(new MemoryStream(bytes));

        var worksheet = workbook.Worksheets[1];

        //int totalRows = worksheet.Cells.MaxRow + 1;
        //int totalColumns = worksheet.Cells.MaxColumn + 1;

        //ExportTableOptions options = new ExportTableOptions();
        //options.PlotVisibleColumns = false;

        //DataTable dataTable = worksheet.Cells.ExportDataTable(0, 0, totalRows, totalColumns, false);


        DataTable dataTable = new();

        bool hasHeader = true;

        for (int i = 0; i < worksheet.Cells.MaxDataColumn; i++)
        {

        }


        //var startRow = hasHeader ? 2 : 1;

        //for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //{
        //    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //    var row = dataTable.NewRow();
        //    foreach (var cell in wsRow)
        //    {
        //        row[cell.Start.Column - 1] = cell.Text;
        //    }
        //    dataTable.Rows.Add(row);
        //}

        return dataTable;
    }
}
