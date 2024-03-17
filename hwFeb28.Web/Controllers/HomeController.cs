using hwFeb28.Data;
using hwFeb28.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hwFeb28.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People; Integrated Security=true;";
        public IActionResult Index()
        {
            var manager = new PeopleDBManager(_connectionString);
            var vm = new PeopleViewModel { People = manager.GetPeople() };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(List<Person> people)
        {
            var manager = new PeopleDBManager(_connectionString);
            int count = manager.AddPeople(people);
            TempData["message"] = $"{count} people added!";
            return Redirect("/");
        }
    }
}
