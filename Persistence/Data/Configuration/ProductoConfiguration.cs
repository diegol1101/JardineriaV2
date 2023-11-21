using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(e => e.CodigoProducto)
            .HasName("PRIMARY");

            builder.ToTable("producto");

            builder.HasIndex(e => e.Gama, "gama");

            builder.Property(e => e.CodigoProducto)
                .HasMaxLength(15)
                .HasColumnName("codigo_producto");
            builder.Property(e => e.CantidadEnStock)
                .HasColumnName("cantidad_en_stock");
            builder.Property(e => e.DescripcionText)
                .HasColumnType("text")
                .HasColumnName("descripcion_text");
            builder.Property(e => e.Dimensiones)
                .HasMaxLength(25)
                .HasColumnName("dimensiones");
            builder.Property(e => e.Gama)
                .HasMaxLength(50)
                .HasColumnName("gama");
            builder.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
            builder.Property(e => e.PrecioProveedor)
                .HasPrecision(15, 2)
                .HasColumnName("precio_proveedor");
            builder.Property(e => e.PrecioVenta)
                .HasPrecision(15, 2)
                .HasColumnName("precio_venta");
            builder.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .HasColumnName("proveedor");

            builder.HasOne(d => d.GamaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Gama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto_ibfk_1");
        }
    }
}