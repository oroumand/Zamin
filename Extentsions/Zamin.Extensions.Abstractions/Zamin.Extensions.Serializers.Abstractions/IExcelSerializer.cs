using System.Data;

namespace Zamin.Extentions.Serializers.Abstractions;
public interface IExcelSerializer
{
    byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result");
    DataTable ExcelToDataTable(byte[] bytes);
    List<T> ExcelToList<T>(byte[] bytes);
}

