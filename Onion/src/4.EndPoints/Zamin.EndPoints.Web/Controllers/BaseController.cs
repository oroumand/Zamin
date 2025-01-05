using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zamin.Utilities;
using Zamin.EndPoints.Web.Extensions;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Core.Contracts.ApplicationServices.Events;

namespace Zamin.EndPoints.Web.Controllers;

public class BaseController : Controller
{
    protected ICommandDispatcher CommandDispatcher => HttpContext.CommandDispatcher();
    protected IQueryDispatcher QueryDispatcher => HttpContext.QueryDispatcher();
    protected IEventDispatcher EventDispatcher => HttpContext.EventDispatcher();
    protected ZaminServices ZaminApplicationContext => HttpContext.ZaminApplicationContext();

    public IActionResult Excel<T>(List<T> list)
    {
        var serializer = HttpContext.RequestServices.GetRequiredService<IExcelSerializer>();
        var bytes = serializer.ListToExcelByteArray(list);
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    public IActionResult Excel<T>(List<T> list, string fileName)
    {
        var serializer = HttpContext.RequestServices.GetRequiredService<IExcelSerializer>();
        var bytes = serializer.ListToExcelByteArray(list);
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }

    [Obsolete("This method is deprecated. Use CreateAsync instead.")]
    protected Task<IActionResult> Create<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
        => CreateAsync<TCommand, TCommandResult>(command);

    protected async Task<IActionResult> CreateAsync<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
    {
        var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.Created, result.Data);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use CreateAsync instead.")]
    protected Task<IActionResult> Create<TCommand>(TCommand command)
        where TCommand : class, ICommand
        => CreateAsync<TCommand>(command);

    protected async Task<IActionResult> CreateAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        var result = await CommandDispatcher.Send(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.Created);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use EditAsync instead.")]
    protected Task<IActionResult> Edit<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
        => EditAsync<TCommand, TCommandResult>(command);

    protected async Task<IActionResult> EditAsync<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
    {
        var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.OK, result.Data);

        if (result.Status is ApplicationServiceStatus.NotFound)
            return StatusCode((int)HttpStatusCode.NotFound, command);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use EditAsync instead.")]
    protected Task<IActionResult> Edit<TCommand>(TCommand command)
        where TCommand : class, ICommand
        => EditAsync<TCommand>(command);

    protected async Task<IActionResult> EditAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        var result = await CommandDispatcher.Send(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.OK);

        if (result.Status is ApplicationServiceStatus.NotFound)
            return StatusCode((int)HttpStatusCode.NotFound, command);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use DeleteAsync instead.")]
    protected Task<IActionResult> Delete<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
        => DeleteAsync<TCommand, TCommandResult>(command);

    protected async Task<IActionResult> DeleteAsync<TCommand, TCommandResult>(TCommand command)
        where TCommand : class, ICommand<TCommandResult>
    {
        var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.OK, result.Data);

        if (result.Status is ApplicationServiceStatus.NotFound)
            return StatusCode((int)HttpStatusCode.NotFound, command);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use DeleteAsync instead.")]
    protected Task<IActionResult> Delete<TCommand>(TCommand command)
        where TCommand : class, ICommand
        => DeleteAsync<TCommand>(command);

    protected async Task<IActionResult> DeleteAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        var result = await CommandDispatcher.Send(command);
        if (result.Status is ApplicationServiceStatus.Ok)
            return StatusCode((int)HttpStatusCode.OK);

        if (result.Status is ApplicationServiceStatus.NotFound)
            return StatusCode((int)HttpStatusCode.NotFound, command);

        return BadRequest(result.Messages);
    }

    [Obsolete("This method is deprecated. Use QueryAsync instead.")]
    protected Task<IActionResult> Query<TQuery, TQueryResult>(TQuery query)
        where TQuery : class, IQuery<TQueryResult>
        => QueryAsync<TQuery, TQueryResult>(query);

    protected async Task<IActionResult> QueryAsync<TQuery, TQueryResult>(TQuery query)
        where TQuery : class, IQuery<TQueryResult>
    {
        var result = await QueryDispatcher.Execute<TQuery, TQueryResult>(query);

        if (result.Status.Equals(ApplicationServiceStatus.InvalidDomainState) ||
            result.Status.Equals(ApplicationServiceStatus.ValidationError))
            return BadRequest(result.Messages);

        if (result.Status.Equals(ApplicationServiceStatus.NotFound) || result.Data == null)
            return StatusCode((int)HttpStatusCode.NoContent);

        if (result.Status.Equals(ApplicationServiceStatus.Ok))
            return Ok(result.Data);

        return BadRequest(result.Messages);
    }
}