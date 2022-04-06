using System.Data;
using System.Reflection;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.EPPlus.Extensions;

public static class LinqExtensions
{
    public static List<T> ToList<T>(this DataTable dt, ITranslator resourceManager)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row, resourceManager);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr, ITranslator resourceManager)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName || resourceManager[pro.Name] == column.ColumnName)
                    pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], pro.PropertyType), null);
                else
                    continue;
            }
        }
        return obj;
    }
}