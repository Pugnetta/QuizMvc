

using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using AspNetMvcClass.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcClass.Controllers;

public class QuizController : Controller
{
    private readonly AuthDbContext dbContext;
    private readonly List<Domanda> _domande;        
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
    public IActionResult Start()
    {
        // Check if this is the first time the user has visited the quiz page
        if (HttpContext.Session.GetInt32("currentQuestionIndex") == null)
        {
            // If it is, initialize the current question index and score to 0
            HttpContext.Session.SetInt32("currentQuestionIndex", 0);
            HttpContext.Session.SetInt32("score", 0);
        }
        int currentQuestionIndex = HttpContext.Session.GetInt32("currentQuestionIndex").Value;           

        // Get the current question using the index
        var currentQuestion = _domande[currentQuestionIndex];

        // Return the view as usual
        return View(currentQuestion);
    }
    [HttpPost]
    public IActionResult Start(string selectedAnswer)
    {
        // Get the current question index and score from the user's session
        int currentQuestionIndex = HttpContext.Session.GetInt32("currentQuestionIndex").Value;
        int score = HttpContext.Session.GetInt32("score").Value;

        // Check if the selected answer is correct
        if (selectedAnswer == _domande[currentQuestionIndex].RispostaEsatta)
        {
            score++;
        }

        // Update the current question index and score in the user's session
        HttpContext.Session.SetInt32("currentQuestionIndex", currentQuestionIndex + 1);
        HttpContext.Session.SetInt32("score", score);

        if (currentQuestionIndex + 1 == _domande.Count)
        {
            // If all the questions have been answered, redirect to the Score action
            return RedirectToAction("Score");
        }
        else
        {
            // If there are more questions, redirect to the Start action
            return RedirectToAction("Start");
        }

    }

    [HttpGet]
    public IActionResult Score()
    {
        // Get the current score from the user's session
        int score = HttpContext.Session.GetInt32("score").Value;

        // Get the current question index from the user's session
        int currentQuestionIndex = HttpContext.Session.GetInt32("currentQuestionIndex").Value;

        if (currentQuestionIndex >= _domande.Count)
        {
            // If all the questions have been answered, reset the current question index and score in the user's session
            HttpContext.Session.SetInt32("currentQuestionIndex", 0);
            HttpContext.Session.SetInt32("score", 0);

            
            return View(score);
        }
        else
        {
            // If not all the questions have been answered, redirect to the error page
            return RedirectToAction("Error", "Home");
        }

    }

}
