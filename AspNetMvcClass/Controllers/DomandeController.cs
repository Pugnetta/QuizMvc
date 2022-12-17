
using AspNetMvcClass.Models;
using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcClass.Controllers;

[Authorize(Roles = "admin")]
public class DomandeController : Controller
{
    private readonly AuthDbContext dbContext;

    public DomandeController(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var domande = await dbContext.Domande.ToListAsync();
        return View(domande);
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddDomandaViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var answerList = new List<string>()
        {
            viewModel.Risposta1,
            viewModel.Risposta2,
            viewModel.Risposta3,
            viewModel.Risposta4
        };
            if (!answerList.Contains(viewModel.RispostaEsatta))
            {
                ModelState.AddModelError("RispostaEsatta", "Deve corrispondere ad almeno una delle altre risposte");
                return View("Add", viewModel);
            }
            var domanda = new Domanda()
            {
                Question = viewModel.Question,
                Risposta1 = viewModel.Risposta1,
                Risposta2 = viewModel.Risposta2,
                Risposta3 = viewModel.Risposta3,
                Risposta4 = viewModel.Risposta4,
                RispostaEsatta = viewModel.RispostaEsatta,
                Categoria = viewModel.Categoria?.ToLower()
            };

            await dbContext.Domande.AddAsync(domanda);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        else
        {
            return View();
        }

    }
    [HttpGet]
    public async Task<IActionResult> View(int id)
    {
       
        var editDomanda = await dbContext.Domande.FirstOrDefaultAsync(x => x.Id == id);
        if (editDomanda != null)
        {
            var viewModel = new EditDomandaViewModel
            {
                Id = editDomanda.Id,
                Question = editDomanda.Question,
                Risposta1 = editDomanda.Risposta1,
                Risposta2 = editDomanda.Risposta2,
                Risposta3 = editDomanda.Risposta3,
                Risposta4 = editDomanda.Risposta4,
                RispostaEsatta = editDomanda.RispostaEsatta,
                Categoria = editDomanda.Categoria?.ToLower()
            };

            return View("View", viewModel);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> View(EditDomandaViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var answerList = new List<string>()
            {
            viewModel.Risposta1,
            viewModel.Risposta2,
            viewModel.Risposta3,
            viewModel.Risposta4
            };
            if (!answerList.Contains(viewModel.RispostaEsatta))
            {
                ModelState.AddModelError("RispostaEsatta", "Deve corrispondere ad almeno una delle altre risposte");
                return View("View", viewModel);
            }
            var domanda = await dbContext.Domande.FindAsync(viewModel.Id);
            domanda.Question = viewModel.Question;
            domanda.Risposta1 = viewModel.Risposta1;
            domanda.Risposta2 = viewModel.Risposta2;
            domanda.Risposta3 = viewModel.Risposta3;
            domanda.Risposta4 = viewModel.Risposta4;
            domanda.RispostaEsatta = viewModel.RispostaEsatta;
            domanda.Categoria = viewModel.Categoria?.ToLower();

            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        else
        {
            return View("View", viewModel);
        }

    }
    [HttpPost]
    public async Task<IActionResult> Delete(EditDomandaViewModel viewModel)
    {
        var domanda = await dbContext.Domande.FindAsync(viewModel.Id);
        if (domanda != null)
        {
            dbContext.Domande.Remove(domanda);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

}
