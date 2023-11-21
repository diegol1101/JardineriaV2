using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
        {
            public void Configure(EntityTypeBuilder<Empleado> builder)
            {
                builder.HasKey(e => e.CodigoEmpleado).HasName("PRIMARY");

            builder.ToTable("empleado");

            builder.HasIndex(e => e.CodigoJefe, "codigo_jefe");

            builder.HasIndex(e => e.CodigoOficina, "codigo_oficina");

            builder.Property(e => e.CodigoEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("codigo_empleado");
            builder.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .HasColumnName("apellido1");
            builder.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .HasColumnName("apellido2");
            builder.Property(e => e.CodigoJefe).HasColumnName("codigo_jefe");
            builder.Property(e => e.CodigoOficina)
                .HasMaxLength(20)
                .HasColumnName("codigo_oficina");
            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            builder.Property(e => e.Extension)
                .HasMaxLength(40)
                .HasColumnName("extension");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            builder.Property(e => e.Puesto)
                .HasMaxLength(60)
                .HasColumnName("puesto");

            builder.HasOne(d => d.CodigoJefeNavigation)
                .WithMany(p => p.InverseCodigoJefeNavigation)
                .HasForeignKey(d => d.CodigoJefe)
                .HasConstraintName("empleado_ibfk_2");

            builder.HasOne(d => d.CodigoOficinaNavigation)
                .WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CodigoOficina)
                .HasConstraintName("empleado_ibfk_1");
            }
        }
}