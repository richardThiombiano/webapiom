namespace WebApiOmTransaction.Models;

public class Utilisateur
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string? Email { get; set; }
    public string? Sexe { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Telephone { get; set; }
    public bool Statut { get; set; }
    public DateTime DateCreation { get; set; }
    public ICollection<Versement> Versements { get; set; }
    public ICollection<Operation> Operations { get; set; }
    public ICollection<Client> Clients { get; set; }
    public int EntrepriseId { get; set; }
    public Entreprise Entreprise { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; } = false;
}