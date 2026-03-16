using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenceImmo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    IdAdresse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodePostal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.IdAdresse);
                });

            migrationBuilder.CreateTable(
                name: "Bien",
                columns: table => new
                {
                    IdBien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeBien = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Surface = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PEB = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    NbreChambre = table.Column<int>(type: "int", nullable: false),
                    NbreFacade = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatutActuel = table.Column<int>(type: "int", nullable: false),
                    DateDisponible = table.Column<DateOnly>(type: "date", nullable: true),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                    IdAdresse = table.Column<int>(type: "int", nullable: true),
                    TypeContrat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bien", x => x.IdBien);
                    table.ForeignKey(
                        name: "FK_Bien_Adresse_IdAdresse",
                        column: x => x.IdAdresse,
                        principalTable: "Adresse",
                        principalColumn: "IdAdresse");
                });

            migrationBuilder.CreateTable(
                name: "Personne",
                columns: table => new
                {
                    IdPersonne = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdAdresse = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personne", x => x.IdPersonne);
                    table.ForeignKey(
                        name: "FK_Personne_Adresse_IdAdresse",
                        column: x => x.IdAdresse,
                        principalTable: "Adresse",
                        principalColumn: "IdAdresse");
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomUtilisateur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Identifiant = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HashPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    IdAdresse = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.IdUtilisateur);
                    table.ForeignKey(
                        name: "FK_Utilisateur_Adresse_IdAdresse",
                        column: x => x.IdAdresse,
                        principalTable: "Adresse",
                        principalColumn: "IdAdresse");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    IdDocument = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomDocument = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeDocument = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<DateOnly>(type: "date", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdBien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.IdDocument);
                    table.ForeignKey(
                        name: "FK_Document_Bien_IdBien",
                        column: x => x.IdBien,
                        principalTable: "Bien",
                        principalColumn: "IdBien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriqueStatut",
                columns: table => new
                {
                    IdHistorique = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateOnly>(type: "date", nullable: false),
                    DateFin = table.Column<DateOnly>(type: "date", nullable: true),
                    IdBien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriqueStatut", x => x.IdHistorique);
                    table.ForeignKey(
                        name: "FK_HistoriqueStatut_Bien_IdBien",
                        column: x => x.IdBien,
                        principalTable: "Bien",
                        principalColumn: "IdBien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appartenance",
                columns: table => new
                {
                    IdAppartenance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonne = table.Column<int>(type: "int", nullable: false),
                    IdBien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appartenance", x => x.IdAppartenance);
                    table.ForeignKey(
                        name: "FK_Appartenance_Bien_IdBien",
                        column: x => x.IdBien,
                        principalTable: "Bien",
                        principalColumn: "IdBien",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appartenance_Personne_IdPersonne",
                        column: x => x.IdPersonne,
                        principalTable: "Personne",
                        principalColumn: "IdPersonne",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    IdEvenement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeEvenement = table.Column<int>(type: "int", nullable: false),
                    DateEvenement = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false),
                    IdBien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.IdEvenement);
                    table.ForeignKey(
                        name: "FK_Evenement_Bien_IdBien",
                        column: x => x.IdBien,
                        principalTable: "Bien",
                        principalColumn: "IdBien",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evenement_Utilisateur_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "IdUtilisateur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipationEvenement",
                columns: table => new
                {
                    IdParticipation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonne = table.Column<int>(type: "int", nullable: false),
                    IdEvenement = table.Column<int>(type: "int", nullable: false),
                    RolePersonne = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipationEvenement", x => x.IdParticipation);
                    table.ForeignKey(
                        name: "FK_ParticipationEvenement_Evenement_IdEvenement",
                        column: x => x.IdEvenement,
                        principalTable: "Evenement",
                        principalColumn: "IdEvenement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipationEvenement_Personne_IdPersonne",
                        column: x => x.IdPersonne,
                        principalTable: "Personne",
                        principalColumn: "IdPersonne",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appartenance_IdBien",
                table: "Appartenance",
                column: "IdBien");

            migrationBuilder.CreateIndex(
                name: "IX_Appartenance_IdPersonne",
                table: "Appartenance",
                column: "IdPersonne");

            migrationBuilder.CreateIndex(
                name: "IX_Bien_IdAdresse",
                table: "Bien",
                column: "IdAdresse");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IdBien",
                table: "Document",
                column: "IdBien");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_IdBien",
                table: "Evenement",
                column: "IdBien");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_IdUtilisateur",
                table: "Evenement",
                column: "IdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriqueStatut_IdBien",
                table: "HistoriqueStatut",
                column: "IdBien");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationEvenement_IdEvenement",
                table: "ParticipationEvenement",
                column: "IdEvenement");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationEvenement_IdPersonne",
                table: "ParticipationEvenement",
                column: "IdPersonne");

            migrationBuilder.CreateIndex(
                name: "IX_Personne_IdAdresse",
                table: "Personne",
                column: "IdAdresse");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_IdAdresse",
                table: "Utilisateur",
                column: "IdAdresse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appartenance");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "HistoriqueStatut");

            migrationBuilder.DropTable(
                name: "ParticipationEvenement");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Personne");

            migrationBuilder.DropTable(
                name: "Bien");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Adresse");
        }
    }
}
