using System;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Repository
{
	public class RoleRepository : IRoleRepository
	{
        private readonly OmOperationContext _omOperationContext;

		public RoleRepository(OmOperationContext omOperationContext)
		{
            _omOperationContext = omOperationContext;
		}

        public ICollection<Role> getRoles()
        {
            return _omOperationContext.Roles.OrderBy(r => r.Id).ToList();
         }


        public bool CheckIfRoleExist(int id)
        {
            return _omOperationContext.Roles.Any(p => p.Id == id);
        }

    }
}

