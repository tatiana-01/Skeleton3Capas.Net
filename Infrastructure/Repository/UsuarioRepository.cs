using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly Skeleton3CapasContext _context;
    public UsuarioRepository(Skeleton3CapasContext context) : base(context)
    {
        _context=context;
    }
    public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    public override async Task<(int totalRegistros, IEnumerable<Usuario> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Usuarios as IQueryable<Usuario>;

        if (!string.IsNullOrEmpty(search)) 
        {
            query = query.Where(p => p.Username.ToLower().Contains(search.ToLower()));
        }
         var totalRegistros=await query.CountAsync();
        var registros = await query
                                .Include(p=>p.Roles)
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
                                
        return (totalRegistros,registros);
    }

    public override async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Set<Usuario>()
        .Include(p => p.Roles)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
