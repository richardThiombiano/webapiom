using System;
namespace WebApiOmTransaction.Models
{
	public class Role
	{
		public int Id { get; set; }
		public string Libelle { get; set; }
		public ICollection<Utilisateur> Utilisateurs { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

