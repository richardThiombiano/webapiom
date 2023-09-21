using System;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository
{
	public class SimCodeAgentRepository : ISimCodeAgentRepository
    {
        private readonly OmOperationContext _omOperationContext;

		public SimCodeAgentRepository(OmOperationContext omOperationContext)
		{
            _omOperationContext = omOperationContext;
		}

        public async Task<bool> AddSimCodeAgent(SimCodeAgentCreateDto simCodeAgentDto)
        {
            await _omOperationContext.AddAsync(new SimCodeAgent
            {
                NumeroAgent = simCodeAgentDto.NumeroAgent,
                CodeAgent = simCodeAgentDto.CodeAgent,
                EntrepriseId = simCodeAgentDto.EntrepriseId,
                IsDeleted = false
            });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public bool CheckIfEntrepriseExist(int entrepriseId)
        {
            var entreprise = _omOperationContext.Entreprises.Where(e => e.Id == entrepriseId).FirstOrDefault();
            if (entreprise != null)
                return true;
            return false;
        }

        public bool CheckIfSimCodeAgentExist(int id)
        {
            return _omOperationContext.SimCodeAgents.Any(p => p.Id == id);
        }

        public async Task<bool> DeleteSimCodeAgent(int simID)
        {
            _omOperationContext.Update(new SimCodeAgent
            {
                Id = simID,
                IsDeleted = true
            });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public ICollection<SimCodeAgent> simCodeAgentsByEntreprise(int entrepriseId)
        {
            return _omOperationContext.SimCodeAgents.Where(s => s.EntrepriseId == entrepriseId && s.IsDeleted == false).ToList();
        }

        public async Task<bool> UpdateSimCodeAgent(SimCodeAgentCreateDto simUpdateDto)
        {
            await _omOperationContext.AddAsync(new SimCodeAgent
            {
                Id = simUpdateDto.Id,
                NumeroAgent = simUpdateDto.NumeroAgent,
                CodeAgent = simUpdateDto.CodeAgent,
                EntrepriseId = simUpdateDto.EntrepriseId,
                IsDeleted = false
            }) ;
            if (await _omOperationContext.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }
    }
}

