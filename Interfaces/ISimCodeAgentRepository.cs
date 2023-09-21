using System;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces
{
	public interface ISimCodeAgentRepository
	{
		ICollection<SimCodeAgent> simCodeAgentsByEntreprise(int entrepriseId);
		Task<bool> AddSimCodeAgent(SimCodeAgentCreateDto simCodeAgentDto);
        bool CheckIfEntrepriseExist(int entrepriseId);
        bool CheckIfSimCodeAgentExist(int id);
        Task<bool> UpdateSimCodeAgent(SimCodeAgentCreateDto simUpdateDto);
        Task<bool> DeleteSimCodeAgent(int simID);
    }
}

