using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces;

public interface IClientRepository
{
    ICollection<Client> GetClients();
    ICollection<Client> GetClientPhysiqueByEntreprise(int IdEntreprise);
    ICollection<Client> GetClientMoralByEntreprise(int IdEntreprise);
    ICollection<Client> GetClientByUtilisateur(int IdUtilisateur);
    Task<bool> AddClientPhysiqueOrMoral(int IdUtilisateur, ClientCreateDto client);
    Client GetClientById(int id);
    bool CheckIfClientExist(int id);
    Task<bool> UpdateClient(ClientCreateDto clientUpdateDto);
    Task<bool> DeleteClient(int clientID);
}