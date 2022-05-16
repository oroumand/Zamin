using System.Data;
using Zamin.Extensions.Serializers.Asposes.Extensions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.Asposes.Services;

public class AsposesExcelSerializer : IExcelSerializer
{

    public AsposesExcelSerializer()
    {
    }

    public byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result") => list.ToExcelByteArray(sheetName);

    public DataTable ExcelToDataTable(byte[] bytes) => bytes.ToDataTableFromExcel();

    public List<T> ExcelToList<T>(byte[] bytes) => bytes.ToDataTableFromExcel().ToList<T>();
}
