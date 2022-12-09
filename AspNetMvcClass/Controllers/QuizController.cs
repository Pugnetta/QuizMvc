

using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using AspNetMvcClass.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcClass.Controllers
{
    public class QuizController : Controller
    {
        private readonly AuthDbContext dbContext;
        private readonly List<Domanda> _domande;
        private static int _currentQuestionIndex = 0;
        private static int _score = 0;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //var user = _httpContextAccessor.HttpContext.User;
        //var userName = user.Identity.Name;

        public QuizController(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
            _domande = dbContext.Domande.ToList();            
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Start()
        {            
            var currentQuestion = _domande[_currentQuestionIndex];
            return View(currentQuestion);
        }
        [HttpPost]
        public IActionResult Start(string selectedAnswer)
        {
            // Check if the selected answer is correct
            if (selectedAnswer == _domande[_currentQuestionIndex].RispostaEsatta)
            {
                _score++;
            }            
            _currentQuestionIndex++;
            if (_currentQuestionIndex == _domande.Count)
            {
                return RedirectToAction("Score", _score);
            }
            else
            {
                return RedirectToAction("Start");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Score()
        {
            var finalScore = _score;
            if (_currentQuestionIndex >= _domande.Count)
            {
                _currentQuestionIndex = 0;
                _score = 0;
                ViewData["message"] = $"Game over! Your final score is {finalScore}.";
                return View(finalScore);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

          
        }




    }
}
