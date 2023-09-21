using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;
using WebApiOmTransaction.Repository;

namespace WebApiOmTransaction.Controllers;

[Route("/api/clients")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepositoryRepository;
    private readonly IMapper _mapper;
    public ClientController(IClientRepository clientRepositoryRepository, IMapper mapper)
    {
        _clientRepositoryRepository = clientRepositoryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerator<Client>))]
    public IActionResult GetClients()
    {
        var clients = _mapper.Map<List<ClientDto>>(_clientRepositoryRepository.GetClients());
        if (!ModelState.IsValid)
            BadRequest(ModelState);
        return Ok(clients);
    }

    [HttpGet("{clientId}")]
    [ProducesResponseType(200, Type = typeof(Client))]
    [ProducesResponseType(400)]
    public IActionResult GetClientById(int clientId)
    {
        if (!_clientRepositoryRepository.CheckIfClientExist(clientId))
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var client = _mapper.Map<ClientDto>(_clientRepositoryRepository.GetClientById(clientId)) ;
        return Ok(client);
    }

    [HttpGet("getclientphysiquebyentreprise/{utilisateurId}")]
    [ProducesResponseType(200, Type = typeof(Client))]
    [ProducesResponseType(400)]
    public IActionResult GetClientPhysiqueByUtilisateurId(int utilisateurId)
    {
        var clients = _mapper.Map<List<ClientDto>>(_clientRepositoryRepository.GetClientPhysiqueByEntreprise(utilisateurId));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(clients);
    }

    [HttpGet("getclientmoralbyentreprise/{utilisateurId}")]
    [ProducesResponseType(200, Type = typeof(Client))]
    [ProducesResponseType(400)]
    public IActionResult GetClientMoralByUtilisateurId(int utilisateurId)
    {
        var clients = _mapper.Map<List<ClientDto>>(_clientRepositoryRepository.GetClientMoralByEntreprise(utilisateurId));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(clients);
    }


    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddClientAsync([FromQuery] int UtilisateurId, [FromBody] ClientCreateDto client)
    {
        if (client == null)
            return BadRequest();
        var clientCreatedByUtilisateur = _clientRepositoryRepository.GetClientByUtilisateur(UtilisateurId)
       .Where(x => x.NumeroDocument.Trim().ToUpper() == client.NumeroDocument.Trim().ToUpper()).FirstOrDefault();

        if(clientCreatedByUtilisateur != null)
        {
            ModelState.AddModelError("", "Client already exist");
            return StatusCode(422, ModelState);
        }

        var result = await _clientRepositoryRepository.AddClientPhysiqueOrMoral(UtilisateurId, client);
        MessageApp mess = new MessageApp
        {
            Message = "Client successfully registred"
        };
        if(result)
            return Ok(mess);
        else
            return StatusCode(500, ModelState);
    }

    [HttpPut("{clientId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(ClientCreateDto))]
    public async Task<IActionResult> UpdateOrDeleteClient(int clientId, [FromBody] ClientCreateDto updatedClient)
    {
        if (updatedClient == null)
            return BadRequest(ModelState);

        if (clientId != updatedClient.Id)
            return BadRequest(ModelState);

        if (!_clientRepositoryRepository.CheckIfClientExist(clientId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _clientRepositoryRepository.UpdateClient(updatedClient);
        if (!result)
        {
            ModelState.AddModelError("", "Something went wrong updating client");
            return StatusCode(500, ModelState);
        }

        MessageApp mess = new MessageApp
        {
            Message = "Client successfully updated"
        };
        return Ok(mess);
    }

}