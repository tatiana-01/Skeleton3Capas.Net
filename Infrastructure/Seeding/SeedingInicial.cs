using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeding;
    public class SeedingInicial
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var AdministradorRol = new Rol()
            {
                Id = 1,
                Nombre = "Administrador"
            };
            var GerenteRol = new Rol()
            {
                Id = 2,
                Nombre = "Gerente"
            };
            var EmpleadoRol = new Rol()
            {
                Id = 3,
                Nombre = "Empleado"
            };
            var PersonaRol = new Rol()
            {
                Id = 4,
                Nombre = "Persona"
            };
            var Administrador = new Usuario()
            {
                Id=1,
                Username="Admin",
                Email="admin@gmail.com",
            };
            var _passwordHasher = new PasswordHasher<Usuario>();
            Administrador.Password = _passwordHasher.HashPassword(Administrador, "123456");
            var AdminUsuarioRol = new UsuarioRol()
            {
                RolId = 1,
                UsuarioId = 1
            };
            modelBuilder.Entity<Usuario>().HasData(Administrador);
            modelBuilder.Entity<Rol>().HasData(AdministradorRol, EmpleadoRol, PersonaRol, GerenteRol);
            modelBuilder.Entity<UsuarioRol>().HasData(AdminUsuarioRol);
        }
    }