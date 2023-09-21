using System;
using WebApiOmTransaction.Models;
using static WebApiOmTransaction.Repository.StatistiqueRepository;

namespace WebApiOmTransaction.Interfaces
{
	public interface IStatistiqueRepository
	{
		double[] GetNombreClientByTypeClient(int EnterpriseId);
        double[] GetNombreOperationByTypeOperation(int EnterpriseId);
    }
}

