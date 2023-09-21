namespace WebApiOmTransaction.Models
{
	public class SimCodeAgent
	{
		public int Id { get; set; }
		public string NumeroAgent { get; set; }
		public string CodeAgent { get; set; }
		public int EntrepriseId { get; set; }
		public Entreprise Entreprise { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

