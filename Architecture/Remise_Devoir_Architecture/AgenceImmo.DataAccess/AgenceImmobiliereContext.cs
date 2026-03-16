using AgenceImmo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgenceImmo.DataAccess;

public class AgenceImmobiliereContext : DbContext
{
    public AgenceImmobiliereContext(DbContextOptions<AgenceImmobiliereContext> options)
        : base(options)
    {
    }

    public DbSet<Adresse> Adresses { get; set; }
    public DbSet<Bien> Biens { get; set; }
    public DbSet<Personne> Personnes { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Appartenance> Appartenances { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Evenement> Evenements { get; set; }
    public DbSet<HistoriqueStatut> HistoriqueStatuts { get; set; }
    public DbSet<ParticipationEvenement> ParticipationsEvenement { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Appartenance : relation Personne <-> Bien
        modelBuilder.Entity<Appartenance>()
            .HasOne(a => a.Personne)
            .WithMany(p => p.Appartenances)
            .HasForeignKey(a => a.IdPersonne)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appartenance>()
            .HasOne(a => a.Bien)
            .WithMany(b => b.Appartenances)
            .HasForeignKey(a => a.IdBien)
            .OnDelete(DeleteBehavior.Restrict);

        // ParticipationEvenement : relation Personne <-> Evenement
        modelBuilder.Entity<ParticipationEvenement>()
            .HasOne(p => p.Personne)
            .WithMany(p => p.Participations)
            .HasForeignKey(p => p.IdPersonne)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParticipationEvenement>()
            .HasOne(p => p.Evenement)
            .WithMany(e => e.Participations)
            .HasForeignKey(p => p.IdEvenement)
            .OnDelete(DeleteBehavior.Restrict);

        // Evenement -> Bien
        modelBuilder.Entity<Evenement>()
            .HasOne(e => e.Bien)
            .WithMany(b => b.Evenements)
            .HasForeignKey(e => e.IdBien)
            .OnDelete(DeleteBehavior.Restrict);

        // Evenement -> Utilisateur
        modelBuilder.Entity<Evenement>()
            .HasOne(e => e.Utilisateur)
            .WithMany(u => u.Evenements)
            .HasForeignKey(e => e.IdUtilisateur)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
