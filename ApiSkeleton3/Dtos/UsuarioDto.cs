using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSkeleton3.Dtos;
public class UsuarioDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    //public ICollection<Rol> Roles { get; set; } = new HashSet<Rol>();
    //public ICollection<UsuarioRol> UsuariosRoles { get; set; }        
}