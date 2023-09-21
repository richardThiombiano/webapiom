using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces;

public interface IUtilisateurRepository
{
     ICollection<UtilisateurDto> GetUtilisateurs();
     ICollection<Versement>? GetVersementByUtilisateur(int utilisateurid);
     Utilisateur GetUtilisateur(int UtilisateurId);
     bool CheckIfUtilisateurExist(int UtilisateurId);
     bool CheckLogin(LoginDto loginDto);
     Task<bool> AddUtilisateur(UtilisateurCreatedDto utilisateur);
     UtilisateurDto GetAuthentificatedUser(LoginDto loginDto);
    Task<bool> UpdateUtilisateur(UtilisateurCreatedDto utilisateurUpdateDto);
    Task<bool> DeleteUtilisateur(UtilisateurCreatedDto updatedUtilisateur);
}