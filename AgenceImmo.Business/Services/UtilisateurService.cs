using System.Security.Cryptography;
using System.Text;
using AgenceImmo.Business.Interfaces;
using AgenceImmo.DataAccess;
using AgenceImmo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenceImmo.Business.Services;

public class UtilisateurService : IUtilisateurService
{
    private readonly AgenceImmobiliereContext _context;

    public UtilisateurService(AgenceImmobiliereContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Vérifie les identifiants et retourne l'utilisateur si l'authentification réussit.
    /// </summary>
    public async Task<Utilisateur?> AuthenticationAsync(string identifiant, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(identifiant))
            throw new ArgumentException("L'identifiant ne peut pas être vide.", nameof(identifiant));
        if (string.IsNullOrWhiteSpace(motDePasse))
            throw new ArgumentException("Le mot de passe ne peut pas être vide.", nameof(motDePasse));

        string hashMotDePasse = HashPassword(motDePasse);

        return await _context.Utilisateurs
            .Include(u => u.Adresse)
            .FirstOrDefaultAsync(u => u.Identifiant == identifiant
                                   && u.HashPassword == hashMotDePasse);
    }

    /// <summary>
    /// Retourne tous les utilisateurs.
    /// </summary>
    public async Task<List<Utilisateur>> GetAllUtilisateursAsync()
    {
        return await _context.Utilisateurs
            .Include(u => u.Adresse)
            .ToListAsync();
    }

    /// <summary>
    /// Crée un nouvel utilisateur en hashant son mot de passe.
    /// </summary>
    public async Task<Utilisateur> CreerUtilisateurAsync(Utilisateur utilisateur, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(utilisateur.Identifiant))
            throw new ArgumentException("L'identifiant de l'utilisateur est obligatoire.");
        if (string.IsNullOrWhiteSpace(motDePasse))
            throw new ArgumentException("Le mot de passe est obligatoire.");

        bool identifiantExiste = await _context.Utilisateurs
            .AnyAsync(u => u.Identifiant == utilisateur.Identifiant);
        if (identifiantExiste)
            throw new InvalidOperationException($"L'identifiant '{utilisateur.Identifiant}' est déjà utilisé.");

        try
        {
            utilisateur.HashPassword = HashPassword(motDePasse);
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
            return utilisateur;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Erreur lors de la création de l'utilisateur.", ex);
        }
    }

    /// <summary>
    /// Hash un mot de passe en SHA-256 et retourne la valeur hexadécimale.
    /// </summary>
    private static string HashPassword(string motDePasse)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(motDePasse));
        return Convert.ToHexString(bytes).ToLower();
    }
}
