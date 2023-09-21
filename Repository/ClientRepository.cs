using AutoMapper;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Enum;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository;

public class ClientRepository : IClientRepository
{
    private readonly OmOperationContext _omOperationContext;
    private readonly IMapper _mapper;
    public ClientRepository(OmOperationContext omOperationContext, IMapper mapper)
    {
        _omOperationContext = omOperationContext;
        _mapper = mapper;
    }

    public ICollection<Client> GetClients()
    {
        var clients = _omOperationContext.Clients.Where(p => p.IsDeleted == false).OrderBy(p => p.Id).ToList();
        return clients;
    }

    public Client GetClientById(int id)
    {
        var client = _omOperationContext.Clients.First(p => p.Id == id);
        return client;
    }

    public bool CheckIfClientExist(int id)
    {
        return _omOperationContext.Clients.Any(p => p.Id == id);
    }

    public ICollection<Client> GetClientPhysiqueByEntreprise(int IdEntreprise)
    {
        var client = _omOperationContext.Clients
            .Where(x => x.EntrepriseId == IdEntreprise && x.RaisonSocial == null && x.IsDeleted == false)
            .OrderByDescending(cl => cl.Id)
            .ToList();
        return client;
    }

    public ICollection<Client> GetClientByUtilisateur(int IdUtilisateur)
    {
        return _omOperationContext.Clients.Where(x => x.UtilisateurId == IdUtilisateur).ToList();
    }

    public ICollection<Client> GetClientMoralByEntreprise(int IdEntreprise)
    {
        var client = _omOperationContext.Clients.Where(x => x.EntrepriseId == IdEntreprise && x.RaisonSocial != null && x.IsDeleted == false)
            .OrderByDescending(cl => cl.Id)
            .ToList();
        return client;
    }

    public TypeDocument getTypeDocument(string type)
    {
        if (type == "CNIB")
            return TypeDocument.CNIB;
        else if (type == "PASSEPORT")
            return TypeDocument.PASSEPORT;
        else
            return TypeDocument.CARTE_DE_SEJOUR;
    }

    public TypeClient getTypeClient(string typeClient)
    {
        if (typeClient == "Moral")
            return TypeClient.Moral;
        else
            return TypeClient.Physique;
    }

    public async Task<bool> AddClientPhysiqueOrMoral(int IdUtilisateur, ClientCreateDto client)
    {
         await _omOperationContext.AddAsync(new Client
         {
             Nom = client.Nom.Trim().ToUpper(),
             Prenom = client.Prenom.Trim(),
             RaisonSocial =  client.RaisonSocial !=null ? client.RaisonSocial.Trim().ToUpper() : null,
             NumeroDocument = client.NumeroDocument.Trim(),
             Sexe = client.Sexe,
             Telephone = client.Telephone.Trim(),
             Email = client.Email.Trim(),
             TypeDocument = getTypeDocument(client.TypeDocument),
             UtilisateurId = IdUtilisateur,
             DateEtablissementDocument = DateOnly.FromDateTime(client.DateEtablissementDocument),
             DateExpirationDocument = DateOnly.FromDateTime(client.DateExpirationDocument),
             TypeClient = getTypeClient(client.TypeClient),
             EntrepriseId = client.EntrepriseId,
             IsDeleted = false
         });
        if (await _omOperationContext.SaveChangesAsync() > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateClient(ClientCreateDto clientUpdateDto)
    {
        _omOperationContext.Update(new Client
        {
            Id = clientUpdateDto.Id,
            Nom = clientUpdateDto.Nom,
            Prenom = clientUpdateDto.Prenom,
            NumeroDocument = clientUpdateDto.NumeroDocument,
            Email = clientUpdateDto.Email,
            Sexe = clientUpdateDto.RaisonSocial,
            UtilisateurId = clientUpdateDto.UtilisateurId,
            Telephone = clientUpdateDto.Telephone,
            DateEtablissementDocument = DateOnly.FromDateTime(clientUpdateDto.DateEtablissementDocument),
            DateExpirationDocument = DateOnly.FromDateTime(clientUpdateDto.DateExpirationDocument),
            RaisonSocial = clientUpdateDto.RaisonSocial,
            TypeDocument = getTypeDocument(clientUpdateDto.TypeDocument),
            EntrepriseId = clientUpdateDto.EntrepriseId,
            IsDeleted = clientUpdateDto.IsDeleted
        });
        var result = await _omOperationContext.SaveChangesAsync();
        if (result > 0)
            return true;
        return false;
    }

    public async Task<bool> DeleteClient(int clientID)
    {
        _omOperationContext.Update(new Client
        {
            Id = clientID,
            IsDeleted = true
        });
        var result = await _omOperationContext.SaveChangesAsync();
        if (result > 0)
            return true;
        return false;
    }
}