using System;
namespace WebApiOmTransaction.Dtos
{
	public class SimCodeAgentCreateDto
	{
        public int Id { get; set; }
        public string NumeroAgent { get; set; }
        public string CodeAgent { get; set; }
        public int EntrepriseId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

