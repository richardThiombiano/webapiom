using System;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Controllers
{
	public class StatistiqueController : ControllerBase
	{
		private readonly IStatistiqueRepository _statistiqueRepository;

		public StatistiqueController(IStatistiqueRepository statistiqueRepository)
		{
			_statistiqueRepository = statistiqueRepository;
		}

		[HttpGet("/api/stats/nbclientbytype/{EnterpriseId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CountByType>))]
		public IActionResult GetStatClientByTypeClient(int EnterpriseId)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(_statistiqueRepository.GetNombreClientByTypeClient(EnterpriseId));
		}

        [HttpGet("/api/stats/nboperationbytype/{EnterpriseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountByType>))]
        public IActionResult GetStatOperationByTypeOperation(int EnterpriseId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_statistiqueRepository.GetNombreOperationByTypeOperation(EnterpriseId));
        }
    }
}
