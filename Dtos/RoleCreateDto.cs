
using System;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Dtos
{
	public class RoleCreateDto
	{
        public int Id { get; set; }
        public string Libelle { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

