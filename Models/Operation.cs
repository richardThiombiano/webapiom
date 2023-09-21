using System.Runtime.InteropServices.JavaScript;
using WebApiOmTransaction.Enum;

namespace WebApiOmTransaction.Models;

public class Operation
{
    public int Id { get; set; }
    public DateTime DateOperation { get; set; }
    public TypeOperation TypeOperation { get; set; }
    public string Numero { get; set; }
    public string? NumeroReceveur { get; set; }
    public float MontantOperation { get; set; }
    public bool StatutPaye { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int UtilisateurId { get; set; }
    public Utilisateur Utilisateur { get; set; }
    public int SimCodeAgentId { get; set; }
    public SimCodeAgent simCodeAgent { get; set; }
    public ICollection<Versement> Versements { get; set; }
    public int EntrepriseId { get; set; }
    public Entreprise Entreprise { get; set; }
    public bool IsDeleted { get; set; } = false;
}