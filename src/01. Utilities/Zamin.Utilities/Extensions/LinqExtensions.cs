using Zamin.Utilities.Services.Localizations;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Zamin.Utilities.Extensions;

public static class LinqExtensions
{
    public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
    {
        var param = Expression.Parameter(typeof(T), "p");
        var prop = Expression.Property(param, SortField);
        var exp = Expression.Lambda(prop, param);
        string method = Ascending ? "OrderBy" : "OrderByDescending";
        Type[] types = new Type[] { q.ElementType, exp.Body.Type };
        var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
        return q.Provider.CreateQuery<T>(mce);
    }

    public static DataTable ToDataTable<T>(this List<T> list)
    {

        DataTable dataTable = new DataTable(typeof(T).Name);

        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in Props)
        {
            dataTable.Columns.Add(prop.Name);
        }

        foreach (T item in list)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows  
                values[i] = Props[i].GetValue(item, null);
            }
            // Finally add value to datatable  
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

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

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
        ? query.Where(predicate)
        : query;
    }

    /// <summary>
    /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
    /// </summary>
    /// <param name="query">Queryable to apply filtering</param>
    /// <param name="condition">A boolean value</param>
    /// <param name="predicate">Predicate to filter the query</param>
    /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
    {
        return condition
        ? query.Where(predicate)
        : query;
    }

    public static IQueryable<T> Paged<T>(this IQueryable<T> query,int skip,int take)
        => query.Skip(skip).Take(take);
}
