using AgenceImmo.Business.Interfaces;
using AgenceImmo.Business.Models;
using AgenceImmo.DataAccess;
using AgenceImmo.DataAccess.Entities;
using AgenceImmo.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace AgenceImmo.Business.Services;

public class BienService : IBienService
{
    private readonly AgenceImmobiliereContext _context;

    public BienService(AgenceImmobiliereContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retourne tous les biens avec leur adresse.
    /// </summary>
    public async Task<List<Bien>> GetAllBiensAsync()
    {
        return await _context.Biens
            .Include(b => b.Adresse)
            .ToListAsync();
    }

    /// <summary>
    /// Retourne un bien par son identifiant.
    /// </summary>
    public async Task<Bien?> GetBienByIdAsync(int idBien)
    {
        if (idBien <= 0)
            throw new ArgumentException("L'identifiant du bien doit être positif.", nameof(idBien));

        return await _context.Biens
            .Include(b => b.Adresse)
            .Include(b => b.Documents)
            .Include(b => b.HistoriqueStatuts)
            .FirstOrDefaultAsync(b => b.IdBien == idBien);
    }

    /// <summary>
    /// Retourne un bien avec ses événements.
    /// </summary>
    public async Task<Bien?> GetBienAvecEvenementsAsync(int idBien)
    {
        if (idBien <= 0)
            throw new ArgumentException("L'identifiant du bien doit être positif.", nameof(idBien));

        return await _context.Biens
            .Include(b => b.Adresse)
            .Include(b => b.Evenements)
            .FirstOrDefaultAsync(b => b.IdBien == idBien);
    }

    /// <summary>
    /// Retourne tous les biens ayant un statut donné.
    /// </summary>
    public async Task<List<Bien>> GetBiensByStatutAsync(StatutBien statut)
    {
        return await _context.Biens
            .Include(b => b.Adresse)
            .Where(b => b.StatutActuel == statut)
            .ToListAsync();
    }

    /// <summary>
    /// Recherche des biens selon des critères combinés (prix, surface, ville, type...).
    /// </summary>
    public async Task<List<Bien>> RechercherBiensAsync(FiltreRechercheBien filtre)
    {
        var query = _context.Biens.Include(b => b.Adresse).AsQueryable();

        if (filtre.TypeBien.HasValue)
            query = query.Where(b => b.TypeBien == filtre.TypeBien.Value);

        if (filtre.TypeContrat.HasValue)
            query = query.Where(b => b.TypeContrat == filtre.TypeContrat.Value);

        if (filtre.Statut.HasValue)
            query = query.Where(b => b.StatutActuel == filtre.Statut.Value);

        if (filtre.PrixMin.HasValue)
            query = query.Where(b => b.Prix >= filtre.PrixMin.Value);

        if (filtre.PrixMax.HasValue)
            query = query.Where(b => b.Prix <= filtre.PrixMax.Value);

        if (filtre.SurfaceMin.HasValue)
            query = query.Where(b => b.Surface >= filtre.SurfaceMin.Value);

        if (filtre.NbChambresMin.HasValue)
            query = query.Where(b => b.NbreChambre >= filtre.NbChambresMin.Value);

        if (!string.IsNullOrWhiteSpace(filtre.Ville))
            query = query.Where(b => b.Adresse != null && b.Adresse.Ville == filtre.Ville);

        return await query.ToListAsync();
    }

    /// <summary>
    /// Retourne les N événements les plus récents toutes propriétés confondues.
    /// </summary>
    public async Task<List<Evenement>> GetEvenementsRecentsAsync(int count)
    {
        return await _context.Evenements
            .Include(e => e.Bien)
            .OrderByDescending(e => e.DateEvenement)
            .Take(count)
            .ToListAsync();
    }

    /// <summary>
    /// Ajoute un nouveau bien dans la base de données.
    /// </summary>
    public async Task<Bien> AjouterBienAsync(Bien bien)
    {
        if (bien.Prix <= 0)
            throw new ArgumentException("Le prix du bien doit être supérieur à zéro.");
        if (bien.Surface <= 0)
            throw new ArgumentException("La surface du bien doit être supérieure à zéro.");
        if (bien.NbreChambre < 0)
            throw new ArgumentException("Le nombre de chambres ne peut pas être négatif.");

        try
        {
            bien.DateCreation = DateOnly.FromDateTime(DateTime.Today);
            _context.Biens.Add(bien);
            await _context.SaveChangesAsync();
            return bien;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Erreur lors de l'ajout du bien en base de données.", ex);
        }
    }

    /// <summary>
    /// Met à jour le statut d'un bien et enregistre l'historique.
    /// </summary>
    public async Task<bool> ChangerStatutBienAsync(int idBien, StatutBien nouveauStatut)
    {
        var bien = await _context.Biens.FindAsync(idBien);
        if (bien == null) return false;

        if (bien.StatutActuel == nouveauStatut)
            throw new InvalidOperationException($"Le bien est déjà au statut '{nouveauStatut}'.");

        try
        {
            var historiqueEnCours = await _context.HistoriqueStatuts
                .FirstOrDefaultAsync(h => h.IdBien == idBien && h.DateFin == null);
            if (historiqueEnCours != null)
                historiqueEnCours.DateFin = DateOnly.FromDateTime(DateTime.Today);

            _context.HistoriqueStatuts.Add(new HistoriqueStatut
            {
                IdBien = idBien,
                Statut = nouveauStatut,
                DateDebut = DateOnly.FromDateTime(DateTime.Today)
            });

            bien.StatutActuel = nouveauStatut;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Erreur lors du changement de statut.", ex);
        }
    }
}
