using System;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Interfaces
{
	public interface  IRoleRepository
	{
		ICollection<Role> getRoles();
        bool CheckIfRoleExist(int id);
    }
}

