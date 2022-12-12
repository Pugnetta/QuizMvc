

using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using AspNetMvcClass.Models.ViewModels;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace AspNetMvcClass.Controllers;

public class QuizController : Controller
{
    private readonly AuthDbContext dbContext;
    
    public QuizController(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;       
    }

    [HttpGet]
    public IActionResult Index()
    {
        HttpContext.Session.Remove("gameSession");
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> InitializeQuizSession()
    {
        // Check if the game session ID exists in the user's session.
        var gameSessionId = GetGameSessionId();
        // Check if the game session with the specified ID exists in the database.
        var gameSessionExists = await dbContext.GameSessions.AnyAsync(gs => gs.Id == gameSessionId);
        if (gameSessionId.HasValue && gameSessionExists)
        {
            return BadRequest();
        }
        string? userName = null;
        if (User.Identity.IsAuthenticated)
        {           
            userName = User.Identity.Name;
        }
        else
        {            
            userName = "Anonymous";
        }
        var gameSession = new GameSession()
        {

            CurrentQuestionIndex = 0,
            Score = 0,
            UserId = userName,
            CreatedAt = DateTime.Now,
            Questions = await dbContext.Domande
            .OrderBy(y => Guid.NewGuid())
            .Take(2)
            .ToListAsync()
        };
       
        await dbContext.GameSessions.AddAsync(gameSession);
        await dbContext.SaveChangesAsync();

        // Store the game session ID in the user's session.
        HttpContext.Session.SetInt32("gameSession", gameSession.Id);

        return RedirectToAction("QuizViews");
    }

    [HttpGet]
    public async Task<IActionResult> QuizViews()
    {
        var gameSessionId = GetGameSessionId();
        if (gameSessionId.HasValue)
        {
            // Retrieve the game session from the database.
            var gameSession = await dbContext.GameSessions
                .Include(gs => gs.Questions)
                .SingleOrDefaultAsync(gs => gs.Id == gameSessionId);

            if (gameSession != null && gameSession.CurrentQuestionIndex < gameSession.Questions.Count)
            {               
                var currentQuestion = gameSession.Questions[gameSession.CurrentQuestionIndex];
                return View(currentQuestion);
            }
            else
            {                
                if (gameSession.CurrentQuestionIndex == gameSession.Questions.Count)
                {                    
                    return RedirectToAction("Score");
                }
            }
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> QuizViews(string answer)
    {
       
        var gameSessionId = GetGameSessionId();
        
        if (gameSessionId.HasValue)
        {
            
            var gameSession = await dbContext.GameSessions
                .Include(gs => gs.Questions)
                .SingleOrDefaultAsync(gs => gs.Id == gameSessionId);
            
            if (gameSession != null && gameSession.CurrentQuestionIndex < gameSession.Questions.Count)
            {                
                var currentQuestion = gameSession.Questions[gameSession.CurrentQuestionIndex];
                var userAnswer = answer;
                
                if (userAnswer == currentQuestion.RispostaEsatta)
                {                    
                    gameSession.Score++;
                }
                
                gameSession.CurrentQuestionIndex++;
               
                dbContext.GameSessions.Update(gameSession);
                await dbContext.SaveChangesAsync();
                
                return RedirectToAction("QuizViews");
            }
        }

        return BadRequest();
    }





    [HttpGet]
    public async Task<IActionResult> Score()
    {
       
        var gameSessionId = GetGameSessionId();
        //Rest user session
        HttpContext.Session.Remove("gameSession");
        
        if (gameSessionId.HasValue)
        {            
            var gameSession = await dbContext.GameSessions
                .SingleOrDefaultAsync(gs => gs.Id == gameSessionId);

            
            if (gameSession != null)
            {
                ScoreViewModel scoreModel = new ScoreViewModel
                {
                    Score = gameSession.Score,
                    TotalQuestions = gameSession.CurrentQuestionIndex
                };
                int res = (int)(gameSession.Score * 0.75);
                if (res < gameSession.Score)
                {
                    return View("Win", scoreModel);
                }
                else
                {
                    return View("Lose", scoreModel);
                }
                
            }
        }

        return BadRequest();
    }



    //private async Task<GameSession> GetGameSessionForUserAsync()
    //{
    //    int? gameSessionId = GetGameSessionId();
    //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    //    return await dbContext.GameSessions
    //        .Include(x => x.Questions)
    //        .FirstOrDefaultAsync(x => x.Id == gameSessionId.Value && x.UserId == userId);
    //}

    private int? GetGameSessionId()
    {
       return HttpContext.Session.GetInt32("gameSession");
       
    }
}


