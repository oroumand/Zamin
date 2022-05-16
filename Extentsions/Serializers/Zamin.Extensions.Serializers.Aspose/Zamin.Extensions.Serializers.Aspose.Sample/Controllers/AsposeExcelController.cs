using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using Zamin.Extensions.Serializers.Asposes.Sample.Models;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.Serializers.Asposes.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AsposeExcelController : ControllerBase
{
    private readonly IExcelSerializer _excelSerializer;

    public AsposeExcelController(IExcelSerializer excelSerializer)
    {
        _excelSerializer = excelSerializer;
    }

    [HttpPut("ToList")]
    public IActionResult ToList(IFormFile file)
    {
        if (file.Length > 0)
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return Ok(_excelSerializer.ExcelToList<ExcelModel>(fileBytes));
            }

        return Ok();
    }

    [HttpGet("ToExcel")]
    public IActionResult ToExcel([FromQuery] List<string> model)
    {
        if (model == null || !model.Any()) return Ok();

        var services = model.Select(s => new ExcelModel() { Title = s }).ToList();

        var result = _excelSerializer.ListToExcelByteArray(services);

        return File(result, "application/vnd.ms-excel", "services.xls");
    }
}