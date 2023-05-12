using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FunTask.Models
{
    public class Usuario: IdentityUser
        {
            public string? NombreCompleto { get; set; }

            public ICollection<Hijo>? Hijos { get; set; }
        }
    
}
