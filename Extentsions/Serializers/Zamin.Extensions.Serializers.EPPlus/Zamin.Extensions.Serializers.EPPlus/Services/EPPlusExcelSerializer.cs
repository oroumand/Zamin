using System.Data;
using Zamin.Extensions.Serializers.EPPlus.Extensions;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Extensions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.EPPlus.Services;

public class EPPlusExcelSerializer : IExcelSerializer
{
    private readonly ITranslator _translator;

    public EPPlusExcelSerializer(ITranslator translator)
    {
        _translator = translator;
    }

    public byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result") => list.ToExcelByteArray(_translator, sheetName);

    public DataTable ExcelToDataTable(byte[] bytes) => bytes.ToDataTableFromExcel();

    public List<T> ExcelToList<T>(byte[] bytes) => bytes.ToDataTableFromExcel().ToList<T>(_translator);
}
