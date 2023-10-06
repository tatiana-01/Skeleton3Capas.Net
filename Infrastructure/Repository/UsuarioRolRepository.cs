using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class UsuarioRolRepository : IUsuarioRol
{
    private readonly Skeleton3CapasContext _context;

    public UsuarioRolRepository(Skeleton3CapasContext context)
    {
        _context = context;
    }

     public virtual void Add(UsuarioRol entity)
        {
            _context.Set<UsuarioRol>().Add(entity);
        }
    
        public virtual void AddRange(IEnumerable<UsuarioRol> entities)
        {
            _context.Set<UsuarioRol>().AddRange(entities);
        }
    
        public virtual IEnumerable<UsuarioRol> Find(Expression<Func<UsuarioRol, bool>> expression)
        {
            return _context.Set<UsuarioRol>().Where(expression);
        }
    
         public virtual async Task<(int totalRegistros, IEnumerable<UsuarioRol> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.UsuarioRoles as IQueryable<UsuarioRol>;
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    
        public virtual async Task<UsuarioRol> GetByIdAsync(int idUsuario, int idRol)
        {
            return await _context.Set<UsuarioRol>().FirstOrDefaultAsync(x=>x.RolId==idRol &&  x.UsuarioId==idUsuario);
        }
    
        public virtual Task<UsuarioRol> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    
        public virtual void Remove(UsuarioRol entity)
        {
            _context.Set<UsuarioRol>().Remove(entity);
        }
    
        public virtual void RemoveRange(IEnumerable<UsuarioRol> entities)
        {
            _context.Set<UsuarioRol>().RemoveRange(entities);
        }
    
        public virtual void Update(UsuarioRol entity)
        {
            _context.Set<UsuarioRol>()
                .Update(entity);
        }
}