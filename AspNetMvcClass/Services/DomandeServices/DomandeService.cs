using AspNetMvcClass.Models;
using AspNetMvcClass.Models.Data;
using AspNetMvcClass.Models.Domain;
using Microsoft.EntityFrameworkCore;



namespace AspNetMvcClass.Services.DomandeServices;

public class DomandeService: IDomandeServices
{
    private readonly AuthDbContext dbContext;
    public DomandeService(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Add(AddDomandaViewModel viewModel)
    {
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
    }

    public async Task Delete(EditDomandaViewModel viewModel)
    {
        var domanda = await dbContext.Domande.FindAsync(viewModel.Id);
        if (domanda != null)
        {
            dbContext.Domande.Remove(domanda);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<Domanda?> GetById(int id)
    {
        return await dbContext.Domande.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(EditDomandaViewModel viewModel)
    {
        var domanda = await dbContext.Domande.FindAsync(viewModel.Id);
        domanda.Question = viewModel.Question;
        domanda.Risposta1 = viewModel.Risposta1;
        domanda.Risposta2 = viewModel.Risposta2;
        domanda.Risposta3 = viewModel.Risposta3;
        domanda.Risposta4 = viewModel.Risposta4;
        domanda.RispostaEsatta = viewModel.RispostaEsatta;
        domanda.Categoria = viewModel.Categoria?.ToLower();

        await dbContext.SaveChangesAsync();
    }

    async Task<List<Domanda>?> IDomandeServices.GetAll()
    {
       return await dbContext.Domande.ToListAsync();
    }
}
