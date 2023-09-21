using System;
namespace WebApiOmTransaction.Dtos
{
	public class RetraitDto
	{
		public string Nom { get; set; }
        public string Prénom { get; set; }
        public string? RaisonSocial { get; set; }
        public string TypeDocument { get; set; }
        public string NumeroDocument { get; set; }
        public string Numero { get; set; }
        public float MontantOperation { get; set; }
        public DateTime DateOperation { get; set; }
        public bool IsDeleted { get; set; }
    }
}

