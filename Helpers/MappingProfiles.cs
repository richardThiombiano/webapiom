using AutoMapper;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Utilisateur, UtilisateurDto>();
        CreateMap<Operation, OperationDto>();
        CreateMap<Entreprise, EntrepriseDto>();
        CreateMap<SimCodeAgent, SimCodeAgentDto>();
    }
}