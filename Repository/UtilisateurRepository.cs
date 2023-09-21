using AutoMapper;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Helpers;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly OmOperationContext _omOperationContext;

    public UtilisateurRepository(OmOperationContext omOperationContext)
    {
        _omOperationContext = omOperationContext;
    }

    public ICollection<UtilisateurDto> GetUtilisateurs()
    {
        var result = (from uti in _omOperationContext.Utilisateurs
            join ent in _omOperationContext.Entreprises
                on uti.EntrepriseId equals ent.Id
            join rol in _omOperationContext.Roles on uti.RoleId equals rol.Id
            where uti.IsDeleted == false
            select new UtilisateurDto
            {
                Nom = uti.Nom,
                Prenom = uti.Prenom,
                RaisonSocial = ent.RaisonSocial,
                Email = uti.Email,
                Role = rol.Libelle,
                Username = uti.Username,
                Password = EncodePasswordToBase64.DecodePassword(uti.Password),
                Entreprise = ent.RaisonSocial,
                Sexe = uti.Sexe,
                DateCreation = uti.DateCreation,
                Telephone = uti.Telephone,
                Statut = uti.Statut,
                Id = uti.Id,
                EntrepriseId = ent.Id,
                RoleId = rol.Id
            }).ToList();
        return result;
    }

    public ICollection<Versement>? GetVersementByUtilisateur(int utilisateurid)
    {
        return _omOperationContext.Utilisateurs.Where(u => u.Id == utilisateurid && u.IsDeleted == false)
            .Select(a => a.Versements).FirstOrDefault();
    }

    public Utilisateur GetUtilisateur(int utilisateurid)
    {
        return _omOperationContext.Utilisateurs.First(u => u.Id == utilisateurid);
    }

    public bool CheckIfUtilisateurExist(int utilisateurid)
    {
        return _omOperationContext.Utilisateurs.Any(u => u.Id == utilisateurid);
    }

    public bool CheckLogin(LoginDto loginDto)
    {
        return _omOperationContext.Utilisateurs.Any(p =>
            p.Username == loginDto.Username && p.Password == EncodePasswordToBase64.EncodePassword(loginDto.Password));
    }

    public UtilisateurDto GetAuthentificatedUser(LoginDto loginDto)
    {
        //return _omOperationContext.Utilisateurs.First(p => p.Username == loginDto.Username  && p.Password == EncodePasswordToBase64.EncodePassword(loginDto.Password));

        var result = (from uti in _omOperationContext.Utilisateurs
            join ent in _omOperationContext.Entreprises
                on uti.EntrepriseId equals ent.Id
            join rol in _omOperationContext.Roles on uti.RoleId equals rol.Id
            where uti.Username == loginDto.Username &&
                  uti.Password == EncodePasswordToBase64.EncodePassword(loginDto.Password)
            select new UtilisateurDto
            {
                Id = uti.Id,
                Nom = uti.Nom,
                Prenom = uti.Prenom,
                Email = uti.Email,
                Role = rol.Libelle,
                Sexe = uti.Sexe,
                Telephone = uti.Telephone,
                Statut = uti.Statut,
                RaisonSocial = ent.RaisonSocial,
                DateCreation = uti.DateCreation,
                EntrepriseId = ent.Id
            }).First();

        return result;
    }

    public async Task<bool> AddUtilisateur(UtilisateurCreatedDto utilisateur)
    {
        await _omOperationContext.AddAsync(new Utilisateur
        {
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Telephone = utilisateur.Telephone,
            Email = utilisateur.Email,
            Sexe = utilisateur.Sexe,
            Statut = utilisateur.Statut,
            RoleId = utilisateur.RoleId,
            Username = utilisateur.Username,
            Password = EncodePasswordToBase64.EncodePassword(utilisateur.Password),
            EntrepriseId = utilisateur.EntrepriseId,
            DateCreation = DateTime.Now
        });

        var result = _omOperationContext.SaveChangesAsync();
        if (await result > 0)
            return true;
        return false;
    }

    public async Task<bool> UpdateUtilisateur(UtilisateurCreatedDto utilisateurUpdateDto)
    {
        _omOperationContext.Update(new Utilisateur
        {
            Id = utilisateurUpdateDto.Id,
            Nom = utilisateurUpdateDto.Nom,
            Prenom = utilisateurUpdateDto.Prenom,
            Telephone = utilisateurUpdateDto.Telephone,
            Email = utilisateurUpdateDto.Email,
            Sexe = utilisateurUpdateDto.Sexe,
            RoleId = utilisateurUpdateDto.RoleId,
            Statut = utilisateurUpdateDto.Statut,
            Username = utilisateurUpdateDto.Username,
            Password = EncodePasswordToBase64.EncodePassword(utilisateurUpdateDto.Password),
            EntrepriseId = utilisateurUpdateDto.EntrepriseId,
            IsDeleted = utilisateurUpdateDto.IsDeleted
        });
        var result = await _omOperationContext.SaveChangesAsync();
        if (result > 0)
            return true;
        return false;
    }

    public async Task<bool> DeleteUtilisateur(UtilisateurCreatedDto updatedUtilisateur)
    {
        _omOperationContext.Update(new Utilisateur
        {
            Id = updatedUtilisateur.Id,
            Nom = updatedUtilisateur.Nom,
            Prenom = updatedUtilisateur.Prenom,
            Telephone = updatedUtilisateur.Telephone,
            Email = updatedUtilisateur.Email,
            Sexe = updatedUtilisateur.Sexe,
            RoleId = updatedUtilisateur.RoleId,
            Statut = updatedUtilisateur.Statut,
            Username = updatedUtilisateur.Username,
            Password = EncodePasswordToBase64.EncodePassword(updatedUtilisateur.Password),
            EntrepriseId = updatedUtilisateur.EntrepriseId,
            IsDeleted = true
        });
        var result = await _omOperationContext.SaveChangesAsync();
        if (result > 0)
            return true;
        return false;
    }
}