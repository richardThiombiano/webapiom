using WebApiOmTransaction.Context;
using WebApiOmTransaction.Enum;
using WebApiOmTransaction.Helpers;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.GenerateData;

public class DatabaseInitializer
{

    public static void Seed(OmOperationContext _omOperationContext)
    {
        var roles = new List<Role>()
        {
            new Role()
            {
                Id = 1,
                Libelle = "Super Administrateur",
                IsDeleted = false
            },
             new Role()
            {
                Id = 2,
                Libelle = "Administrateur",
                IsDeleted = false
            },
                new Role()
            {
                Id = 3,
                Libelle = "Standard",
                IsDeleted = false
            }
        };

        var entreprises = new List<Entreprise>()
            {
                new Entreprise()
                {
                    Id = 1,
                    NomResponsable = "ZABRE",
                     PrenomResponsable ="Parfait",
                     RaisonSocial = "AMS",
                     Adresse = "Wemtenga",
                     Email="parfaait@gmail.com",
                     Telephone = "64343432"
                }
            };

        var utilisateurs = new List<Utilisateur>()
            {
                new Utilisateur()
                {
                    Id = 1,
                    Username = "momo",
                    Password = EncodePasswordToBase64.EncodePassword("momo"),
                    Statut = true,
                    DateCreation = DateTime.Now,
                    Telephone = "07259999",
                    Sexe = "Homme",
                    Email = "mo.thiombiano@gmail.com",
                    Nom = "THIOMBIANO",
                    Prenom = "Mohamed",
                    EntrepriseId = 1,
                    RoleId = 2,
                    IsDeleted = false
                }
            };

        var simCodeAgent = new List<SimCodeAgent>() {
                new SimCodeAgent()
                {
                    Id = 1,
                    EntrepriseId = 1,
                    NumeroAgent = "54675644",
                    CodeAgent = "10957",
                      IsDeleted = false
                },
                new SimCodeAgent()
                {
                    Id = 2,
                    EntrepriseId = 1,
                    NumeroAgent = "75983953",
                    CodeAgent = "10536",
                      IsDeleted = false
                },

        };


        var clients = new List<Client>()
            {
                new Client()
                {
                    Id = 1,
                    Nom = "Thiombiano",
                    Prenom = "Mohamed",
                    Telephone = "22676260192",
                    Sexe = "Homme",
                    Email = "mo@outlook.fr",
                    TypeDocument = TypeDocument.CNIB, NumeroDocument = "B1235475",
                    DateEtablissementDocument = DateOnly.FromDateTime(DateTime.Now),
                    DateExpirationDocument = DateOnly.FromDateTime(DateTime.Now),
                    UtilisateurId = 1,
                    EntrepriseId = 1,
                      IsDeleted = false
                }
            };

        var operations = new List<Operation>()
            {
                new Operation()
                {
                    Id = 1,
                    DateOperation = DateTime.Now,
                    TypeOperation = TypeOperation.Depot,
                    MontantOperation = 50000,
                    NumeroReceveur = "55234342",
                    Numero = "74182345",
                    SimCodeAgentId = 1,
                    StatutPaye = true,
                    UtilisateurId = 1,
                    ClientId = 1,
                    EntrepriseId= 1,
                      IsDeleted = false
                }
            };



        var versements = new List<Versement>()
            {
                new Versement()
                {
                    Id = 1,
                    UtilisateurId = 1,
                    ClientId = 1,
                    OperationId = 1,
                    MontantVerse = 50000,
                    DateVersement = DateTime.Now,
                    TypeVersement = TypeVersement.LIQUIDITE,
                    NumeroVersement = "V00001",
                      IsDeleted = false
                }
            };
        _omOperationContext.Roles.AddRange(roles);
        _omOperationContext.Entreprises.AddRange(entreprises);
        _omOperationContext.SimCodeAgents.AddRange(simCodeAgent);
        _omOperationContext.Utilisateurs.AddRange(utilisateurs);
        _omOperationContext.Clients.AddRange(clients);
        _omOperationContext.Operations.AddRange(operations);
        _omOperationContext.Versements.AddRange(versements);
        _omOperationContext.SaveChanges();

    }

}