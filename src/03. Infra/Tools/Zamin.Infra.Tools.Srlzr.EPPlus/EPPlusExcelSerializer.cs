﻿using Zamin.Utilities.Services.Localizations;
using Zamin.Utilities.Services.Serializers;
using System.Data;
using Zamin.Utilities.Extensions;

namespace Zamin.Infra.Tools.Srlzr.EPPlus;
public class EPPlusExcelSerializer : IExcelSerializer
{
    private readonly ITranslator _resourceManager;

    public EPPlusExcelSerializer(ITranslator resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public byte[] ListToExcelByteArray<T>(List<T> list, string sheetName = "Result")
    {
        return list.ToExcelByteArray(_resourceManager, sheetName);
    }

    public DataTable ExcelToDataTable(byte[] bytes)
    {
        return bytes.ToDataTableFromExcel();
    }

    public List<T> ExcelToList<T>(byte[] bytes)
    {
        var dt = bytes.ToDataTableFromExcel();
        var lst = dt.ToList<T>(_resourceManager);
        return lst;
    }
}
