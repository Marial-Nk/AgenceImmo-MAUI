using AgenceImmo.Business.Models;
using AgenceImmo.Business.Services;
using AgenceImmo.DataAccess;
using AgenceImmo.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// =========================================================
//  Lecture de la connection string depuis appsettings.json
// =========================================================
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string connectionString = config.GetConnectionString("AgenceImmobiliere")
    ?? throw new InvalidOperationException("Connection string 'AgenceImmobiliere' introuvable.");

var options = new DbContextOptionsBuilder<AgenceImmobiliereContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new AgenceImmobiliereContext(options);

// =========================================================
//  Test 1 : Accès direct à l'Entity Data Model
// =========================================================
Console.WriteLine("=== Test 1 : Entity Data Model (accès direct EF Core) ===");
var tousLesBiens = await context.Biens.Include(b => b.Adresse).ToListAsync();
Console.WriteLine($"Nombre de biens dans la base : {tousLesBiens.Count}");
foreach (var b in tousLesBiens.Take(3))
{
    Console.WriteLine($"  [{b.IdBien}] {b.TypeBien} - {b.Prix:C} - Statut: {b.StatutActuel}");
    if (b.Adresse != null)
        Console.WriteLine($"       Adresse : {b.Adresse.Rue}, {b.Adresse.Ville}");
}

// =========================================================
//  Test 2 : BienService — méthodes de base
// =========================================================
Console.WriteLine();
Console.WriteLine("=== Test 2 : BienService ===");
var bienService = new BienService(context);

var biens = await bienService.GetAllBiensAsync();
Console.WriteLine($"GetAllBiensAsync()         -> {biens.Count} bien(s)");

var biensDisponibles = await bienService.GetBiensByStatutAsync(StatutBien.Disponible);
Console.WriteLine($"GetBiensByStatutAsync(Disponible) -> {biensDisponibles.Count} bien(s)");

if (tousLesBiens.Count > 0)
{
    var bien = await bienService.GetBienByIdAsync(tousLesBiens.First().IdBien);
    Console.WriteLine($"GetBienByIdAsync({tousLesBiens.First().IdBien})       -> {bien?.TypeBien.ToString() ?? "non trouvé"}");
}

// =========================================================
//  Test 3 : BienService — recherche avancée
// =========================================================
Console.WriteLine();
Console.WriteLine("=== Test 3 : Recherche avancée ===");
var filtre = new FiltreRechercheBien
{
    TypeContrat = TypeContrat.Vente,
    PrixMax = 500000,
    NbChambresMin = 2
};
var resultats = await bienService.RechercherBiensAsync(filtre);
Console.WriteLine($"Ventes <= 500 000€, min 2 chambres -> {resultats.Count} résultat(s)");

// =========================================================
//  Test 4 : PersonneService
// =========================================================
Console.WriteLine();
Console.WriteLine("=== Test 4 : PersonneService ===");
var personneService = new PersonneService(context);
var personnes = await personneService.GetAllPersonnesAsync();
Console.WriteLine($"GetAllPersonnesAsync()     -> {personnes.Count} personne(s)");

// =========================================================
//  Test 5 : UtilisateurService
// =========================================================
Console.WriteLine();
Console.WriteLine("=== Test 5 : UtilisateurService ===");
var utilisateurService = new UtilisateurService(context);
var utilisateurs = await utilisateurService.GetAllUtilisateursAsync();
Console.WriteLine($"GetAllUtilisateursAsync()  -> {utilisateurs.Count} utilisateur(s)");

// Test authentification (avec des identifiants fictifs)
var userAuth = await utilisateurService.AuthenticationAsync("admin", "password123");
Console.WriteLine($"AuthenticationAsync(admin) -> {(userAuth != null ? $"Connecté : {userAuth.NomUtilisateur}" : "Échec d'authentification")}");

Console.WriteLine();
Console.WriteLine("Tests terminés. Appuyez sur une touche pour quitter.");
Console.ReadKey();
