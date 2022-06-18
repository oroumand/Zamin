using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using Zamin.Infra.Data.Sql.Commands.Extensions;
namespace Zamin.Infra.Data.Sql.Commands.Interceptors;

public class SetPersianYeKeInterceptor: DbCommandInterceptor
{
    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        command.ApplyCorrectYeKe();
        return base.NonQueryExecuting(command, eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        command.ApplyCorrectYeKe();
        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }
    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        command.ApplyCorrectYeKe();
        return base.ReaderExecuting(command, eventData, result);
    }
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        command.ApplyCorrectYeKe();
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
    public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        command.ApplyCorrectYeKe();
        return base.ScalarExecuting(command, eventData, result);
    }
    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        command.ApplyCorrectYeKe();
        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

}
