using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeleton3.Dtos;
using Core.Entities;

namespace ApiSkeleton3.Services;
public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    //Task<string> RegisterAsync(RegisterDto registerDto, int opcionPersona, int personaId);
    Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);
    Task<string> AddRolAsync(AddRolDto model);
    Task<Usuario> EditUserAsync(Usuario model);
    Task<DatosUsuarioDto> RefreshTokenAsync(string refreshToken);
}