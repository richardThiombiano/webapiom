using System;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository
{
    public class EntrepriseRepository : IEntrepriseRepository
    {
        private readonly OmOperationContext _omOperationContext;

        public EntrepriseRepository(OmOperationContext omOperationContext)
        {
            _omOperationContext = omOperationContext;
        }

        public async Task<bool> AddEntreprise(EntrepriseCreateDto entrepriseCreateDto)
        {
            await _omOperationContext.AddAsync(new Entreprise
            {
                Id = entrepriseCreateDto.Id,
                NomResponsable = entrepriseCreateDto.NomResponsable,
                PrenomResponsable = entrepriseCreateDto.PrenomResponsable,
                RaisonSocial = entrepriseCreateDto.RaisonSocial,
                Adresse = entrepriseCreateDto.Adresse,
                Email = entrepriseCreateDto.Email,
                Telephone = entrepriseCreateDto.Telephone,
                IsDeleted = false
            });

            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public bool checkIfEntrepriseExist(int entrepriseId)
        {
            return _omOperationContext.Entreprises.Any(p => p.Id == entrepriseId);
        }

        public ICollection<Entreprise> GetEntreprises()
        {
            return _omOperationContext.Entreprises.Where(p => p.IsDeleted == false).OrderBy(e => e.RaisonSocial).ToList();
        }

        public Entreprise GetEntrepriseById(int EntrepriseId)
        {
            return _omOperationContext.Entreprises.Where(e => e.Id == EntrepriseId).First();
        }

        public async Task<bool> UpdateEntreprise(EntrepriseCreateDto entrepriseUpdateDto)
        {
           _omOperationContext.Update(new Entreprise
           {
               Id = entrepriseUpdateDto.Id,
               NomResponsable = entrepriseUpdateDto.NomResponsable,
               PrenomResponsable = entrepriseUpdateDto.PrenomResponsable,
               Adresse = entrepriseUpdateDto.Adresse,
               Email = entrepriseUpdateDto.Email,
               RaisonSocial = entrepriseUpdateDto.RaisonSocial,
               Telephone = entrepriseUpdateDto.Telephone,
               IsDeleted = false
           });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }


        public async Task<bool> DeleteEntreprise(EntrepriseCreateDto entrepriseDeleteDto)
        {
            _omOperationContext.Update(new Entreprise
            {
                Id = entrepriseDeleteDto.Id,
                NomResponsable = entrepriseDeleteDto.NomResponsable,
                PrenomResponsable = entrepriseDeleteDto.PrenomResponsable,
                Adresse = entrepriseDeleteDto.Adresse,
                Email = entrepriseDeleteDto.Email,
                RaisonSocial = entrepriseDeleteDto.RaisonSocial,
                Telephone = entrepriseDeleteDto.Telephone,
                IsDeleted = true
            });
            var result = await _omOperationContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
    }
}

