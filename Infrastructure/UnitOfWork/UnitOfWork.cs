using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Skeleton3CapasContext context;
    private UsuarioRepository _usuarios;
    private RolRepository _roles;
    private UsuarioRolRepository _usuarioRoles;

    public UnitOfWork(Skeleton3CapasContext _context){
        context=_context;
    }
    public IRol Roles {
        get{
            if(_roles == null){
                _roles = new RolRepository(context);
            }
            return _roles;
        }
    }

    public IUsuario Usuarios { 
        get{
            if(_usuarios == null){
                _usuarios = new UsuarioRepository(context);
            }
            return _usuarios;
        }
    }

    public IUsuarioRol UsuarioRoles { 
        get{
            if(_usuarioRoles == null){
                _usuarioRoles = new UsuarioRolRepository(context);
            }
            return _usuarioRoles;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public Task<int> SaveAsync()
    {
        return context.SaveChangesAsync();
    }
}