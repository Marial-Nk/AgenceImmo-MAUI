using AgenceImmo.DataAccess.Entities;

namespace AgenceImmo.Business.Interfaces;

public interface IUtilisateurService
{
    Task<Utilisateur?> AuthenticationAsync(string identifiant, string motDePasse);
    Task<List<Utilisateur>> GetAllUtilisateursAsync();
    Task<Utilisateur> CreerUtilisateurAsync(Utilisateur utilisateur, string motDePasse);
}
