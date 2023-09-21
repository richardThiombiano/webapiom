using WebApiOmTransaction.Enum;

namespace WebApiOmTransaction.Models;

public class Client
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public TypeClient TypeClient { get; set; }
    public string? RaisonSocial { get; set; }
    public string Sexe { get; set; }
    public TypeDocument TypeDocument { get; set; }
    public string NumeroDocument { get; set; }
    public DateOnly? DateEtablissementDocument { get; set; }
    public DateOnly? DateExpirationDocument { get; set; }
    public int UtilisateurId { get; set; }
    public Utilisateur Utilisateur { get; set; }
    public ICollection<Versement> Versements { get; set; }
    public ICollection<Operation> Operations { get; set; }
    public int EntrepriseId { get; set; }
    public Entreprise Entreprise { get; set; }
    public bool IsDeleted { get; set; } = false;

}