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

        DataTable dataTable = new();

        bool hasHeader = true;

        foreach (Cell item in worksheet.Cells.Rows[0])
        {
            dataTable.Columns.Add(hasHeader ? item.StringValue : string.Format("Column {0}", item.Name));
        }

        if (hasHeader)
            worksheet.Cells.Rows.RemoveAt(0);

        foreach (Row item in worksheet.Cells.Rows)
        {
            var dr = dataTable.NewRow();
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                dr[i] = item[i].StringValue;
            }
            dataTable.Rows.Add(dr);
        }

        return dataTable;
    }
}
