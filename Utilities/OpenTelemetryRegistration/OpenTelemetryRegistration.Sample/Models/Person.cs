using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zamin.Utilities.OpenTelemetryRegistration.Sample.Models
{
    public class Person
    {
        [BindNever]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
