using WebApiOmTransaction.Enum;

namespace WebApiOmTransaction.Models;

public class Versement
{
    public int Id { get; set; }
    public DateTime DateVersement { get; set; }
    public string NumeroVersement { get; set; }
    public float MontantVerse { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public TypeVersement TypeVersement { get; set; }
    public int UtilisateurId { get; set; }
    public Utilisateur Utilisateur { get; set; }
    public int OperationId { get; set; }
    public Operation Operation { get; set; }
    public bool IsDeleted { get; set; } = false;
}