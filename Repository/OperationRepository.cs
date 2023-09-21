using AutoMapper;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Enum;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository
{
	public class OperationRepository : IOperationRepository
	{
        private readonly OmOperationContext _omOperationContext;
        private readonly IMapper _mapper;

		public OperationRepository(OmOperationContext omOperationContext, IMapper mapper)
		{
            _omOperationContext = omOperationContext;
            _mapper = mapper;
		}

        public TypeOperation getTypeOperation (string typeOperation)
        {
            if (typeOperation == "Retrait")
                return TypeOperation.Retrait;
            else
                return TypeOperation.Depot;
        } 

        public async Task<bool> AddRetraitOperation(int idUtilisateur, OperationCreatedDto operationCreatedDto)
        {
            await _omOperationContext.AddAsync(new Operation
            {
                ClientId = operationCreatedDto.ClientId,
                DateOperation = operationCreatedDto.DateOperation,
                MontantOperation = operationCreatedDto.MontantOperation,
                Numero = operationCreatedDto.Numero,
                NumeroReceveur = operationCreatedDto.NumeroReceveur != null ? operationCreatedDto.NumeroReceveur.Trim().ToUpper() : null,
                UtilisateurId = idUtilisateur,
                TypeOperation = getTypeOperation(operationCreatedDto.TypeOperation),
                StatutPaye = operationCreatedDto.StatutPaye,
                SimCodeAgentId = operationCreatedDto.SimCodeAgentId,
                EntrepriseId = operationCreatedDto.EntrepriseId,
                IsDeleted = false
            });

            if (await _omOperationContext.SaveChangesAsync() > 0)
                return true;
            else
                return false;

        }

        public static string getTypeDocument(TypeDocument type)
        {
            if (type == TypeDocument.CNIB)
                return "CNIB";
            else if (type == TypeDocument.PASSEPORT)
                return "PASSEPORT";
            else
                return "CARTE DE SEJOUR";
        }

        public ICollection<OperationDto> GetListeRetrait(int idEntreprise)
        {
             var result = (from op in _omOperationContext.Operations
                           join cli in _omOperationContext.Clients on op.ClientId equals cli.Id
                          join uti in _omOperationContext.Utilisateurs on op.UtilisateurId equals uti.Id
                          join simc in _omOperationContext.SimCodeAgents on op.SimCodeAgentId equals simc.Id
                          where uti.EntrepriseId.Equals(idEntreprise) && op.TypeOperation.Equals(TypeOperation.Retrait) && op.IsDeleted.Equals(false)
                          orderby op.DateOperation descending
                          select new OperationDto
                          {
                              Id = op.Id,
                              Nom = cli.Nom,
                              Prenom = cli.Prenom,
                              Email = cli.Email,
                              TypeDocument = getTypeDocument(cli.TypeDocument),
                              DateOperation = op.DateOperation,
                              NumeroDocument = cli.NumeroDocument,
                              CodeAgent = simc.CodeAgent,
                              NumeroReceveur = op.NumeroReceveur,
                              Numero = op.Numero,
                              MontantOperation = op.MontantOperation,
                              StatutPaye = op.StatutPaye
                          }).ToList();
            return result;
        }

        public ICollection<OperationDto> GetListeDepot(int idEntreprise)
        {
            var result = (from op in _omOperationContext.Operations
                          join cli in _omOperationContext.Clients on op.ClientId equals cli.Id
                          join uti in _omOperationContext.Utilisateurs on op.UtilisateurId equals uti.Id
                          join simc in _omOperationContext.SimCodeAgents on op.SimCodeAgentId equals simc.Id
                          where uti.EntrepriseId.Equals(idEntreprise) && op.TypeOperation.Equals(TypeOperation.Depot) && op.IsDeleted.Equals(false)
                          orderby op.DateOperation descending
                          select new OperationDto
                          {
                              Id = op.Id,
                              Nom = cli.Nom,
                              Prenom = cli.Prenom,
                              Email = cli.Email,
                              TypeDocument = getTypeDocument(cli.TypeDocument),
                              SimAgent = simc.NumeroAgent,
                              NumeroDocument = cli.NumeroDocument,
                              DateOperation = op.DateOperation,
                              NumeroReceveur = op.NumeroReceveur,
                              Numero = op.Numero,
                              MontantOperation = op.MontantOperation,
                              StatutPaye = op.StatutPaye,
                              SimCodeAgentId = op.SimCodeAgentId
                          }).ToList();
            return result;
        }


        public async Task<bool> UpdateOperation(OperationCreatedDto operationUpdateDto)
        {
            _omOperationContext.Update(new Operation
            {
                ClientId = operationUpdateDto.ClientId,
                DateOperation = operationUpdateDto.DateOperation,
                MontantOperation = operationUpdateDto.MontantOperation,
                Numero = operationUpdateDto.Numero,
                NumeroReceveur = operationUpdateDto.NumeroReceveur != null ? operationUpdateDto.NumeroReceveur.Trim().ToUpper() : null,
                UtilisateurId = operationUpdateDto.UtilisateurId,
                TypeOperation = getTypeOperation(operationUpdateDto.TypeOperation),
                StatutPaye = operationUpdateDto.StatutPaye,
                SimCodeAgentId = operationUpdateDto.SimCodeAgentId,
                EntrepriseId = operationUpdateDto.EntrepriseId,
                IsDeleted = operationUpdateDto.IsDeleted
            });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteOperation(int operationId)
        {
            _omOperationContext.Update(new Operation
            {
                Id = operationId,
                IsDeleted = true
            });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public bool CheckIfOperationExist(int id)
        {
            return _omOperationContext.Operations.Any(p => p.Id == id);
        }
    }
}

