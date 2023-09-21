using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;
using WebApiOmTransaction.Repository;

namespace WebApiOmTransaction.Controllers
{
    [Route("api/entreprises")]
    [ApiController]
    public class EntrepriseController : Controller
	{
		private readonly IEntrepriseRepository _entrepriseRepository;
        private readonly IMapper _mapper;

		public EntrepriseController(IEntrepriseRepository entrepriseRepository, IMapper mapper)
		{
			_entrepriseRepository = entrepriseRepository;
            _mapper = mapper;
		}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerator<EntrepriseDto>))]
        public IActionResult GetClients()
        {
            var entreprises = _mapper.Map<List<EntrepriseDto>>(_entrepriseRepository.GetEntreprises());
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(entreprises);
        }


        [HttpGet("{entrepriseId}")]
        [ProducesResponseType(200, Type = typeof(EntrepriseDto))]
        [ProducesResponseType(400)]
        public IActionResult GetEntrepriseById(int entrepriseId)
        {
            if (!_entrepriseRepository.checkIfEntrepriseExist(entrepriseId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var entreprise = _mapper.Map<EntrepriseDto>(_entrepriseRepository.GetEntrepriseById(entrepriseId));
            return Ok(entreprise);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(EntrepriseCreateDto))]
        public async Task<IActionResult> AjouterEntreprise([FromBody] EntrepriseCreateDto entrepriseCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _entrepriseRepository.AddEntreprise(entrepriseCreateDto);

            MessageApp mess = new MessageApp
            {
                Message = "Entreprise enregistrée avec succées"
            };
            if (result)
                return Ok(mess);
            else
                return StatusCode(500, ModelState);
        }




        [HttpPut("{entrepriseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(EntrepriseCreateDto))]
        public async Task<IActionResult> UpdateEntreprise(int entrepriseId, [FromBody] EntrepriseCreateDto updatedEntreprise)
        {
            if (updatedEntreprise == null)
                return BadRequest(ModelState);

            if(entrepriseId != updatedEntreprise.Id)
                return BadRequest(ModelState);

            if(!_entrepriseRepository.checkIfEntrepriseExist(entrepriseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _entrepriseRepository.UpdateEntreprise(updatedEntreprise);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating entreprise");
                return StatusCode(500, ModelState);
            }
  
            MessageApp mess = new MessageApp
            {
                Message = "Entreprise mis à jour avec succées"
            };
            return Ok(mess);
        }

        [HttpPut("/api/delete/entreprise/{entrepriseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(EntrepriseCreateDto))]
        public async Task<IActionResult> DeleteEntreprise(int entrepriseId, EntrepriseCreateDto updatedEntreprise)
        {
            if (updatedEntreprise is null)
                return BadRequest(ModelState);

            if(entrepriseId != updatedEntreprise.Id)
                return BadRequest(ModelState);

            if(!_entrepriseRepository.checkIfEntrepriseExist(entrepriseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _entrepriseRepository.DeleteEntreprise(updatedEntreprise);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong deleting entreprise");
                return StatusCode(500, ModelState);
            }
  
            MessageApp mess = new MessageApp
            {
                Message = "Entreprise supprimée avec succées"
            };
            return Ok(mess);
        }

    }
}

