using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Controllers
{
    [Route("/api/operations")]
    [ApiController]
    public class OperationController : ControllerBase
	{
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;

        public OperationController(IOperationRepository operationRepository, IMapper mapper)
		{
            _operationRepository = operationRepository;
			_mapper = mapper;
        }

        [HttpGet("/api/listeretraits/{entrepriseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperationDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetClientOperationRetraitByUtilisateur(int entrepriseId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_operationRepository.GetListeRetrait(entrepriseId));
        }

        [HttpGet("/api/listedepots/{entrepriseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperationDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetClientOperationDepotByUtilisateur(int entrepriseId)
        {
            var listedepots = _operationRepository.GetListeDepot(entrepriseId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(listedepots);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EnregisterOperation([FromQuery] int idUtilisateur, [FromBody] OperationCreatedDto operationCreatedDto)
        {
            var result = await _operationRepository.AddRetraitOperation(idUtilisateur, operationCreatedDto);
            MessageApp mess = new MessageApp
            {
                Message = "Opération bien enregistrée avec succées"
            };
            if (result)
                return Ok(mess);
            else
                return StatusCode(500, ModelState);

        }

        [HttpPut("{operationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(OperationCreatedDto))]
        public async Task<IActionResult> UpdateOrDeleteOperation(int operationId, [FromBody] OperationCreatedDto updatedOperation)
        {
            if (updatedOperation == null)
                return BadRequest(ModelState);

            if (operationId != updatedOperation.Id)
                return BadRequest(ModelState);

            if (!_operationRepository.CheckIfOperationExist(operationId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _operationRepository.UpdateOperation(updatedOperation);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating operation");
                return StatusCode(500, ModelState);
            }

            MessageApp mess = new MessageApp
            {
                Message = "Operation successfully updated"
            };
            return Ok(mess);
        }

    }

}

