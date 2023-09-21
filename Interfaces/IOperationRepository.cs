using System;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces
{
	public interface IOperationRepository
	{
        Task<bool> AddRetraitOperation(int idUtilisateur, OperationCreatedDto operationCreatedDto);
        ICollection<OperationDto> GetListeRetrait(int idEntreprise);
        ICollection<OperationDto> GetListeDepot(int idEntreprise);
        Task<bool> UpdateOperation(OperationCreatedDto operationUpdated);
        bool CheckIfOperationExist(int id);
    }
}

