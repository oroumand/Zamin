using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zamin.Extentions.ObjectMappers.Abstractions;
using Zamin.Extentions.ObjectMappers.AutoMapper.Sample.Models;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoMapperController : ControllerBase
    {
        private readonly IMapperAdapter _mapperAdapter;
        public AutoMapperController(IMapperAdapter mapperAdapter)
        {
            _mapperAdapter = mapperAdapter;
        }
        [HttpGet("MapPersonToStudent")]
        public IActionResult MapPersonToStudent([FromQuery]Person model)
        {
            var student = _mapperAdapter.Map<Person, Student>(model);
            return Ok(student);
        }

        [HttpGet("MapStudentToPerson")]
        public IActionResult MapStudentToPerson([FromQuery] Student model)
        {
            var person = _mapperAdapter.Map<Student, Person>(model);
            return Ok(person);
        }
    }
}
