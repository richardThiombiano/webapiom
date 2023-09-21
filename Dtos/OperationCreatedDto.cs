using System;
using WebApiOmTransaction.Enum;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Dtos
{
	public class OperationCreatedDto
	{
        public int Id { get; set; }
        public DateTime DateOperation { get; set; }
        public required string TypeOperation { get; set; }
        public required string Numero { get; set; }
        public string? NumeroReceveur { get; set; }
        public float MontantOperation { get; set; }
        public bool StatutPaye { get; set; }
        public int ClientId { get; set; }
        public int UtilisateurId { get; set; }
        public int EntrepriseId { get; set; }
        public int SimCodeAgentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

