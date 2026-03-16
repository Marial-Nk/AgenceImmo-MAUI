using AgenceImmo.Business.Interfaces;
using AgenceImmo.DataAccess;
using AgenceImmo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenceImmo.Business.Services;

public class PersonneService : IPersonneService
{
    private readonly AgenceImmobiliereContext _context;

    public PersonneService(AgenceImmobiliereContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retourne toutes les personnes avec leur adresse.
    /// </summary>
    public async Task<List<Personne>> GetAllPersonnesAsync()
    {
        return await _context.Personnes
            .Include(p => p.Adresse)
            .ToListAsync();
    }

    /// <summary>
    /// Retourne une personne par son identifiant.
    /// </summary>
    public async Task<Personne?> GetPersonneByIdAsync(int idPersonne)
    {
        if (idPersonne <= 0)
            throw new ArgumentException("L'identifiant de la personne doit être positif.", nameof(idPersonne));

        return await _context.Personnes
            .Include(p => p.Adresse)
            .Include(p => p.Appartenances)
                .ThenInclude(a => a.Bien)
            .FirstOrDefaultAsync(p => p.IdPersonne == idPersonne);
    }

    /// <summary>
    /// Retourne la liste des biens appartenant à une personne donnée.
    /// </summary>
    public async Task<List<Bien>> GetBiensByPersonneAsync(int idPersonne)
    {
        if (idPersonne <= 0)
            throw new ArgumentException("L'identifiant de la personne doit être positif.", nameof(idPersonne));

        var personneExiste = await _context.Personnes.AnyAsync(p => p.IdPersonne == idPersonne);
        if (!personneExiste)
            throw new KeyNotFoundException($"Aucune personne trouvée avec l'identifiant {idPersonne}.");

        return await _context.Appartenances
            .Where(a => a.IdPersonne == idPersonne)
            .Include(a => a.Bien)
                .ThenInclude(b => b.Adresse)
            .Select(a => a.Bien)
            .ToListAsync();
    }
}
