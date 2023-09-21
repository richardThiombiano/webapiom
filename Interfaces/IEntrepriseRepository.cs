using System;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces
{
	public interface IEntrepriseRepository
	{
		ICollection<Entreprise> GetEntreprises();
		Task<bool> AddEntreprise(EntrepriseCreateDto entrepriseCreateDto);
		Entreprise GetEntrepriseById(int EntrepriseId);
		bool checkIfEntrepriseExist(int EntrepriseId);
        Task<bool> UpdateEntreprise(EntrepriseCreateDto entrepriseCreateDto);
        Task<bool> DeleteEntreprise(EntrepriseCreateDto entrepriseCreateDto);
    }
}

