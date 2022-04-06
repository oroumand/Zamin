using System.Data;
using Zamin.Extensions.Serializers.EPPlus.Extensions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.EPPlus.Services;

public class EPPlusExcelSerializer : IExcelSerializer
{
    private readonly ITranslator _resourceManager;

    public EPPlusExcelSerializer(ITranslator resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result") => list.ToExcelByteArray(_resourceManager, sheetName);

    public DataTable ExcelToDataTable(byte[] bytes) => bytes.ToDataTableFromExcel();

    public List<T> ExcelToList<T>(byte[] bytes) => bytes.ToDataTableFromExcel().ToList<T>(_resourceManager);
}
