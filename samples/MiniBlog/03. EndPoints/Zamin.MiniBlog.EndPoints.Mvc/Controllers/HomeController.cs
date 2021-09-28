using Zamin.MiniBlog.EndPoints.Mvc.Models;
using Zamin.EndPoints.Web.Controllers;
using Zamin.MiniBlog.Core.ApplicationServices.People.Commands.CreatePerson;
using Zamin.MiniBlog.EndPoints.Mvc.Infrastrucutres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Zamin.MiniBlog.EndPoints.Mvc.Controllers
{
    //public class Person
    //{
    //    public int Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public int Age { get; set; }
    //}

    //public class PersonValidator : AbstractValidator<Person>
    //{
    //    public PersonValidator()
    //    {
    //        RuleFor(x => x.Id).NotNull();
    //        RuleFor(x => x.FirstName).Length(0, 10);
    //        RuleFor(x => x.LastName).EmailAddress();
    //        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    //    }
    //}

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public ICustomServiceSingletone CustomServiceSingletone { get; }

        public HomeController(ILogger<HomeController> logger, ICustomServiceSingletone customServiceSingletone)
        {
            _logger = logger;
            CustomServiceSingletone = customServiceSingletone;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Test()
        {
            //var result = await CommandDispatcher.Send<CreatePersonCommand, long>(new CreatePersonCommand
            //{
            //    FirstName = "Alrieza" + DateTime.Now.Ticks,
            //    LastName = "Alrieza" + DateTime.Now.Ticks
            //});
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Session()
        {
            var sessionValue = GetTestSessionValue();
            return View(sessionValue as object);
        }
        public IActionResult SetSession()
        {
            var sessionKey = "test-session";
            HttpContext.Session.Set(sessionKey, System.Text.Encoding.UTF8.GetBytes("Hi this is test session value"));
            return RedirectToAction(nameof(Session));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Create()
        {
            return View(new CreatePersonCommand());
        }

        [HttpPost]
        public IActionResult Create(CreatePersonCommand person)
        {

            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return View("Create", person);
            }

            Save(person); //Save the person to the database, or some other logic

            TempData["notice"] = "Person successfully created";
            return RedirectToAction("Index");

        }

        private void Save(CreatePersonCommand person)
        {
            Console.WriteLine("");
        }
        private string GetTestSessionValue()
        {
            var sessionKey = "test-session";
            return HttpContext.Session.TryGetValue(sessionKey, out byte[] sessionValue) ?
                System.Text.Encoding.UTF8.GetString(sessionValue) :
                "";
        }
    }
}
