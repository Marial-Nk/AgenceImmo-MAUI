using AgenceImmo.Business.Models;
using AgenceImmo.DataAccess.Entities;
using AgenceImmo.DataAccess.Enums;

namespace AgenceImmo.Business.Interfaces;

public interface IBienService
{
    Task<List<Bien>> GetAllBiensAsync();
    Task<Bien?> GetBienByIdAsync(int idBien);
    Task<Bien?> GetBienAvecEvenementsAsync(int idBien);
    Task<List<Bien>> GetBiensByStatutAsync(StatutBien statut);
    Task<List<Bien>> RechercherBiensAsync(FiltreRechercheBien filtre);
    Task<Bien> AjouterBienAsync(Bien bien);
    Task<bool> ChangerStatutBienAsync(int idBien, StatutBien nouveauStatut);
    Task<List<Evenement>> GetEvenementsRecentsAsync(int count);
}
