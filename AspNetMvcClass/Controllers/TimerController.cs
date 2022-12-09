using AspNetMvcClass.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcClass.Controllers
{
    public class TimerController : Controller
    {
        private readonly AuthDbContext dbContext;

        public TimerController(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
