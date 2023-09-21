using System;
namespace WebApiOmTransaction.Models
{
	public class Entreprise
	{
		public int Id { get; set; }
		public string RaisonSocial { get; set; }
		public string NomResponsable { get; set; }
		public string PrenomResponsable { get; set; }
		public string Telephone{ get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public ICollection<SimCodeAgent> SimCodeAgents { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public ICollection<Utilisateur> Utilisateurs { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

