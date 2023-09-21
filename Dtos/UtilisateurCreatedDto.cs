using System;
namespace WebApiOmTransaction.Dtos
{
	public class UtilisateurCreatedDto
	{
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Sexe { get; set; }
        public string? Telephone { get; set; }
        public bool Statut { get; set; }
        public DateTime DateCreation { get; set; }
        public int EntrepriseId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

