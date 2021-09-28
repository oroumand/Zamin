using System.Collections.Generic;
using System.Data;

namespace Zamin.Utilities.Services.Serializers
{
    public interface IExcelSerializer
    {
        byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result");
        DataTable ExcelToDataTable(byte[] bytes);
        List<T> ExcelToList<T>(byte[] bytes);
    }
}
