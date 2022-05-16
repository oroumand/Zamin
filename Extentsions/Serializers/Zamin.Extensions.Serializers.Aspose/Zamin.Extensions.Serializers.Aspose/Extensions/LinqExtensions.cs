using System.Data;
using System.Reflection;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Serializers.Asposes.Extensions;

public static class LinqExtensions
{
    public static List<T> ToList<T>(this DataTable dataTable)
    {
        List<T> data = new();

        foreach (DataRow row in dataTable.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }

        return data;
    }

    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);

        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName || pro.Name == column.ColumnName)
                    pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], pro.PropertyType), null);
                else
                    continue;
            }
        }

        return obj;
    }
}