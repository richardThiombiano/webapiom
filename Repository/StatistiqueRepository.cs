using System;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository
{
	public class StatistiqueRepository : IStatistiqueRepository
    {
        private readonly OmOperationContext _omOperationContext;

		public StatistiqueRepository(OmOperationContext omOperationContext)
		{
            _omOperationContext = omOperationContext;
		}


        public double[] GetNombreClientByTypeClient(int EnterpriseId)
        {
            var result = (_omOperationContext.Clients
                .Where(c => c.EntrepriseId == EnterpriseId && c.IsDeleted == false)
                .GroupBy(cli => cli.TypeClient)
                          .Select(group => new CountByType
                          {
                              nombre = group.Count()
                          })).ToArray();
            var donnees = new double[2];
            for(int i = 0; i < result.Length; i++)
            {
                donnees[i] = result[i].nombre;
            }
            return donnees;
        }

        public double[] GetNombreOperationByTypeOperation(int EnterpriseId)
        {
            var result = (_omOperationContext.Operations
                .Where(o => o.EntrepriseId == EnterpriseId && o.IsDeleted == false)
                .GroupBy(o => o.TypeOperation)
                .Select(group => new CountByType
                {
                    nombre = group.Count()
                })).ToArray();
            var donnees = new double[2];
            for (int i = 0; i < result.Length; i++)
            {
                donnees[i] = result[i].nombre;
            }
            return donnees;
        }
    }
}

