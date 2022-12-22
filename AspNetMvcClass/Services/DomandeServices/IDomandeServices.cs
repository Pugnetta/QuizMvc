using AspNetMvcClass.Models;
using AspNetMvcClass.Models.Domain;

namespace AspNetMvcClass.Services.DomandeServices
{
    public interface IDomandeServices
    {
        Task<List<Domanda>?> GetAll();
        Task Add(AddDomandaViewModel viewModel);
        Task<Domanda?> GetById(int id);
        Task Update(EditDomandaViewModel viewModel);
        Task Delete(EditDomandaViewModel viewModel);

    }
}
