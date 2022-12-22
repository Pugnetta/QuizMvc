
using AspNetMvcClass.Models;
using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using AspNetMvcClass.Services.DomandeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcClass.Controllers;

[Authorize(Roles = "admin")]
public class DomandeController : Controller
{
    private readonly AuthDbContext dbContext;
    private readonly IDomandeServices services;

    public DomandeController(AuthDbContext dbContext, IDomandeServices domandeServices)
    {
        this.dbContext = dbContext;
        this.services = domandeServices;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var domande = await services.GetAll();
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
            await services.Add(viewModel);
            TempData["SuccessMessage"] = "La domanda è stata aggiunta correttamente.";
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
       
        var editDomanda = await services.GetById(id);
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
        return NotFound();
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
            await services.Update(viewModel);
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
        return BadRequest("Index");
    }

}
