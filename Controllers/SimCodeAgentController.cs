using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Controllers
{
	[Route("/api/simcodeagents")]
	[ApiController]
	public class SimCodeAgentController : ControllerBase
	{
		private readonly ISimCodeAgentRepository _simCodeAgentRepository;
		private readonly IMapper _mapper;
		public SimCodeAgentController(ISimCodeAgentRepository simCodeAgentRepository, IMapper mapper)
		{
            _simCodeAgentRepository = simCodeAgentRepository;
			_mapper = mapper;
		}

		[HttpGet("/api/listesimcodeagentsbyentreprise/{entrepriseId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<SimCodeAgentDto>))]
		public IActionResult GetListSimCodeAgentByEnterpise(int entrepriseId)
		{
			if (!_simCodeAgentRepository.CheckIfEntrepriseExist(entrepriseId))
				return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _mapper.Map<List<SimCodeAgentDto>>(_simCodeAgentRepository.simCodeAgentsByEntreprise(entrepriseId));
			return Ok(result);
		}


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(SimCodeAgentCreateDto))]
        public async Task<IActionResult> AddSimCodeAgent([FromBody] SimCodeAgentCreateDto simCodeAgentCreateDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var result = await _simCodeAgentRepository.AddSimCodeAgent(simCodeAgentCreateDto);
            MessageApp mess = new MessageApp
            {
                Message = "Sim et Code agent enregistré avec succées"
            };
            if (result)
                return Ok(mess);
            else
                return StatusCode(500, ModelState);
		}

        [HttpPut("{simId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(SimCodeAgentCreateDto))]
        public async Task<IActionResult> UpdateOrDeleteSim(int simId, [FromBody] SimCodeAgentCreateDto simCodeAgent)
        {
            if (simCodeAgent == null)
                return BadRequest(ModelState);

            if (simId != simCodeAgent.Id)
                return BadRequest(ModelState);

            if (!_simCodeAgentRepository.CheckIfSimCodeAgentExist(simId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _simCodeAgentRepository.UpdateSimCodeAgent(simCodeAgent);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating sim agent");
                return StatusCode(500, ModelState);
            }

            MessageApp mess = new MessageApp
            {
                Message = "Sim Agent successfully updated"
            };
            return Ok(mess);
        }
    }
}

