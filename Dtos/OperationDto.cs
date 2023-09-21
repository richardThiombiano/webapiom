using System;
namespace WebApiOmTransaction.Dtos
{
	public class OperationDto
	{
        public int Id { get; set; }
        public DateTime DateOperation { get; set; }
        public  string Operation { get; set; }
        public  string Numero { get; set; }
        public string Nom { get; set; }
        public string TypeDocument { get; set; }
        public string SimAgent { get; set; }
        public string CodeAgent { get; set; }
        public string Email { get; set; }
        public string Prenom { get; set; }
        public string? NumeroReceveur { get; set; }
        public string NumeroDocument { get; set; }
        public float MontantOperation { get; set; }
        public bool StatutPaye { get; set; }
        public int SimCodeAgentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

