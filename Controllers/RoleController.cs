using Microsoft.AspNetCore.Mvc;
using WebApiOmTransaction.Dtos;
using WebApiOmTransaction.Interfaces;

namespace WebApiOmTransaction.Controllers
{
    [Route("/api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
	{
		private  readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleCreateDto>))]
        public IActionResult GetAllRoles()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _roleRepository.getRoles();
            return Ok(result);
        }
    }
}

