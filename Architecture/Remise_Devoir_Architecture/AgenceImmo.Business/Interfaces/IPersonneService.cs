using AgenceImmo.DataAccess.Entities;

namespace AgenceImmo.Business.Interfaces;

public interface IPersonneService
{
    Task<List<Personne>> GetAllPersonnesAsync();
    Task<Personne?> GetPersonneByIdAsync(int idPersonne);
    Task<List<Bien>> GetBiensByPersonneAsync(int idPersonne);
}
