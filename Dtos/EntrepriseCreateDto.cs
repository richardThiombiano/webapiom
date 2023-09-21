using System;
namespace WebApiOmTransaction.Dtos
{
	public class EntrepriseCreateDto
	{
        public int Id { get; set; }
        public string RaisonSocial { get; set; }
        public string NomResponsable { get; set; }
        public string PrenomResponsable { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}

