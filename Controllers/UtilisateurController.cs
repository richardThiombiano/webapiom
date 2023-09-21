using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Controllers;

[Route("api/utilisateurs")]
[ApiController]
public class UtilisateurController : ControllerBase
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IMapper _mapper;

    public UtilisateurController(IUtilisateurRepository utilisateurRepository, IMapper mapper)
    {
        _utilisateurRepository = utilisateurRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UtilisateurDto>))]
    public IActionResult GetUtilisateurs()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var utilisateurs = _utilisateurRepository.GetUtilisateurs().ToList();
        return Ok(utilisateurs);
    }

    [HttpGet("/{utilisateurid}")]
    [ProducesResponseType(200, Type = typeof(Utilisateur))]
    [ProducesResponseType(400)]
    public IActionResult GetUtilisateurById(int utilisateurid)
    {
        if (!_utilisateurRepository.CheckIfUtilisateurExist(utilisateurid))
            return NotFound();
        var utilisateur = _mapper.Map<UtilisateurDto>(_utilisateurRepository.GetUtilisateur(utilisateurid));
        return Ok(utilisateur);
    }

    [HttpGet("/versementbyutilisateur/{utilisateurid}")]
    [ProducesResponseType(200, Type = typeof(Utilisateur))]
    public IActionResult GetVersementByUtilisateurId(int utilisateurid)
    {
        if (!_utilisateurRepository.CheckIfUtilisateurExist(utilisateurid))
            return NotFound();
        var versements = _utilisateurRepository.GetVersementByUtilisateur(utilisateurid);
        return Ok(versements);
    }

    [HttpPost()]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(UtilisateurCreatedDto))]
    public async Task<IActionResult> AddUtilisateurAsync([FromBody] UtilisateurCreatedDto utilisateur)
    {
        if (utilisateur == null)
            return BadRequest();

        var result = await _utilisateurRepository.AddUtilisateur(utilisateur);
        MessageApp mess = new MessageApp
        {
            Message = "Utilisateur enregistré avec succées"
        };
        if (result)
            return Ok(mess);
        else
            return StatusCode(500, ModelState);
    }

    [HttpPut("{utilisateurId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(UtilisateurCreatedDto))]
    public async Task<IActionResult> UpdateClient(int utilisateurId, [FromBody] UtilisateurCreatedDto updatedUtilisateur)
    {
        if (updatedUtilisateur == null)
            return BadRequest(ModelState);

        if (utilisateurId != updatedUtilisateur.Id)
            return BadRequest(ModelState);

        if (!_utilisateurRepository.CheckIfUtilisateurExist(utilisateurId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _utilisateurRepository.UpdateUtilisateur(updatedUtilisateur);
        if (!result)
        {
            ModelState.AddModelError("", "Something went wrong updating utilisateur");
            return StatusCode(500, ModelState);
        }

        MessageApp mess = new MessageApp
        {
            Message = "Utilisateur successfully updated"
        };
        return Ok(mess);
    }
    
    
    [HttpPut("/api/delete/utilisateur/{utilisateurId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(UtilisateurCreatedDto))]
    public async Task<IActionResult> DeleteClient(int utilisateurId, [FromBody] UtilisateurCreatedDto updatedUtilisateur)
    {
        if (updatedUtilisateur == null)
            return BadRequest(ModelState);

        if (utilisateurId != updatedUtilisateur.Id)
            return BadRequest(ModelState);

        if (!_utilisateurRepository.CheckIfUtilisateurExist(utilisateurId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _utilisateurRepository.DeleteUtilisateur(updatedUtilisateur);
        if (!result)
        {
            ModelState.AddModelError("", "Something went wrong deleting utilisateur");
            return StatusCode(500, ModelState);
        }

        MessageApp mess = new MessageApp
        {
            Message = "Utilisateur successfully deleted"
        };
        return Ok(mess);
    }


}