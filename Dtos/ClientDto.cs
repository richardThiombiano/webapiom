using WebApiOmTransaction.Enum;

namespace WebApiOmTransaction.Dtos;

public class ClientDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string? RaisonSocial { get; set; }
    public string? Sexe { get; set; }
    public string? TypeDocument { get; set; }
    public string? TypeClient { get; set; }
    public string? NumeroDocument { get; set; }
    public DateOnly? DateEtablissementDocument { get; set; }
    public DateOnly? DateExpirationDocument { get; set; }
    public bool IsDeleted { get; set; }
}