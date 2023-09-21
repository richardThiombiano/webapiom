using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;

namespace WebApiOmTransaction.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController : Controller
{
    private IConfiguration _config;
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IMapper _mapper;

    public LoginController(IConfiguration config, IUtilisateurRepository utilisateurRepository, IMapper mapper)
    {
        _config = config;
        _utilisateurRepository = utilisateurRepository;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody]LoginDto login)
    {
        IActionResult response = Unauthorized();
        var user = AuthenticateUser(login);

        if (user != null)
        {

            var tokenString = GenerateJSONWebToken();
            user.Token = tokenString;
            response = Ok(user);
        }
        return response;
    }

    private string GenerateJSONWebToken()
    {
        var Key = "f06c3c8db2c24cc69a1a8b3b3c82b9f0";
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
    private UtilisateurDto? AuthenticateUser(LoginDto loginDto)
    {
        //Validate the User Credentials
        //Demo Purpose, I have Passed HardCoded User Information
        if (!_utilisateurRepository.CheckLogin(loginDto))
            return null;
        return  _mapper.Map<UtilisateurDto>(_utilisateurRepository.GetAuthentificatedUser(loginDto));
    }
}