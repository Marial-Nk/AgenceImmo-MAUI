-- ============================================================
--  Données de test pour AgenceImmobiliere
-- ============================================================

USE AgenceImmobiliere;
GO

-- ------------------------------------------------------------
--  1. Adresses
-- ------------------------------------------------------------
INSERT INTO Adresse (Rue, CodePostal, Ville, Pays) VALUES
('Rue de la Loi 1',       '1000', 'Bruxelles', 'Belgique'),
('Avenue Louise 42',      '1050', 'Bruxelles', 'Belgique'),
('Rue du Commerce 15',    '4000', 'Liège',     'Belgique'),
('Chaussée de Waterloo 8','1180', 'Uccle',     'Belgique');
GO

-- ------------------------------------------------------------
--  2. Utilisateur (agent immobilier — requis par Evenement)
-- ------------------------------------------------------------
INSERT INTO Utilisateur (NomUtilisateur, Prenom, Identifiant, HashPassword, Role) VALUES
('Dupont', 'Marie', 'mdupont', 'hash_password_123', 1);  -- Role 1 = Agent
GO

-- ------------------------------------------------------------
--  3. Biens
-- ------------------------------------------------------------
-- TypeBien  : 1=Maison, 2=Appartement, 3=Studio, 4=Villa, 5=Terrain
-- StatutBien: 1=Disponible, 2=EnNegociation, 3=Vendu, 4=Loue, 5=Retire
-- TypeContrat: 1=Vente, 2=Location, 3=LocationVente

INSERT INTO Bien (TypeBien, Prix, Surface, PEB, NbreChambre, NbreFacade, Description, StatutActuel, DateCreation, DateDisponible, IdAdresse, TypeContrat) VALUES
(
    2,         
    285000.00,
    85.50,
    'B',
    2, 1,
    'Bel appartement lumineux au 3e étage avec terrasse et parking. Vue dégagée.',
    1,          -- Disponible
    '2025-01-10',
    '2025-02-01',
    1,          
    1           -- Vente
),
(
    1,          -- Maison
    450000.00,
    145.00,
    'C',
    4, 3,
    'Maison 4 façades avec jardin et garage double. Quartier calme proche écoles.',
    2,          -- EnNegociation
    '2025-03-05',
    NULL,
    2,          -- Avenue Louise
    1           -- Vente
),
(
    3,          -- Studio
    750.00,
    32.00,
    'D',
    0, 1,
    'Studio entièrement meublé, idéal étudiant. Proche transports en commun.',
    1,          -- Disponible
    '2025-11-20',
    '2026-02-01',
    3,          -- Rue du Commerce
    2           -- Location
),
(
    4,          -- Villa
    895000.00,
    280.00,
    'A',
    5, 4,
    'Villa de standing avec piscine, grand jardin paysager et dépendances.',
    3,          -- Vendu
    '2024-06-01',
    NULL,
    4,          -- Chaussée de Waterloo
    1           -- Vente
);
GO

-- ------------------------------------------------------------
--  4. Événements
--  TypeEvenement: 1=Visite, 2=Offre, 3=SignatureCompromis,
--                 4=SignatureActe, 5=EtatDesLieux, 6=Autre
-- ------------------------------------------------------------

-- Événements pour le bien 1
INSERT INTO Evenement (TypeEvenement, DateEvenement, Description, IdUtilisateur, IdBien) VALUES
(6, '2025-01-12', 'Mise en ligne du bien',          1, 1),
(1, '2025-01-20', 'Visite couple intéressé',        1, 1),
(1, '2025-01-28', 'Seconde visite confirmée',       1, 1),
(2, '2025-02-03', 'Offre reçue à 275 000 EUR',      1, 1);

-- Événements pour le bien 2 (Maison — en négociation)
INSERT INTO Evenement (TypeEvenement, DateEvenement, Description, IdUtilisateur, IdBien) VALUES
(6, '2025-03-06', 'Publication annonce',            1, 2),
(1, '2025-03-15', 'Première visite famille',        1, 2),
(2, '2025-03-22', 'Offre à 440 000 EUR',            1, 2);

-- Événements pour le bien 3 (Studio — location)
INSERT INTO Evenement (TypeEvenement, DateEvenement, Description, IdUtilisateur, IdBien) VALUES
(6, '2025-11-21', 'Mise en ligne location',         1, 3),
(1, '2025-12-02', 'Visite locataire potentiel',     1, 3);

-- Événements pour le bien 4 (Villa — vendu)
INSERT INTO Evenement (TypeEvenement, DateEvenement, Description, IdUtilisateur, IdBien) VALUES
(6, '2024-06-02', 'Publication annonce premium',   1, 4),
(1, '2024-07-10', 'Visite acheteur sérieux',        1, 4),
(2, '2024-08-01', 'Offre acceptée à 890 000 EUR',   1, 4),
(3, '2024-09-15', 'Signature du compromis',         1, 4),
(4, '2024-11-30', 'Signature acte notarié',         1, 4);
GO

-- ------------------------------------------------------------
--  Vérification rapide
-- ------------------------------------------------------------
SELECT b.IdBien, b.TypeBien, b.StatutActuel, b.Prix, a.Ville
FROM Bien b
LEFT JOIN Adresse a ON b.IdAdresse = a.IdAdresse;

SELECT e.IdEvenement, e.TypeEvenement, e.DateEvenement, e.Description, e.IdBien
FROM Evenement e
ORDER BY e.IdBien, e.DateEvenement;
GO
