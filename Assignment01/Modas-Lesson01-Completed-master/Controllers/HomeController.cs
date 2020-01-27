using Microsoft.AspNetCore.Mvc;
using Modas.Models;

namespace Modas.Controllers
{
    public class HomeController : Controller
    {
        //public string Index() => "This is POCO 2";
        //public ViewResult Index() => View();
        private IEventRepository repository;
        public HomeController(IEventRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Events);
    }
}
