using Core;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuracion;
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Username)
            .HasColumnType("varchar")
            .HasMaxLength(50);

            builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(255);

            builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

            builder
            .HasMany(p=>p.Roles)
            .WithMany(p=>p.Usuarios)
            .UsingEntity<UsuarioRol>(
                j=>j
                .HasOne(p=>p.Rol)
                .WithMany(p=>p.UsuarioRoles)
                .HasForeignKey(p=>p.RolId),

                j=>j
                .HasOne(p=>p.Usuario)
                .WithMany(p=>p.UsuarioRoles)
                .HasForeignKey(p=>p.UsuarioId),

                j=>{
                    j.ToTable("UsuarioRol");
                    j.HasKey(p=> new{p.UsuarioId, p.RolId});
                });
                
                builder.HasMany(p=>p.RefreshTokens)
                .WithOne(p=>p.Usuario)
                .HasForeignKey(p=>p.UsuarioId);
         
        }
    }