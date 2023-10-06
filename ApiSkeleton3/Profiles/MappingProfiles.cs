using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeleton3.Dtos;
using AutoMapper;
using Core.Entities;

namespace ApiSkeleton3.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<Rol, RolPostDto>().ReverseMap();
        CreateMap<Rol, RolGetAllDto>().ReverseMap();

        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        CreateMap<Usuario, UsuarioGetAllDto>().ReverseMap();

        CreateMap<UsuarioRol, UsuarioRolDto>().ReverseMap();
    }
}