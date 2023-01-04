using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace locacao_veiculo
{
    public partial class GERVEICULOSContext : DbContext
    {
        public GERVEICULOSContext()
        {
        }

        public GERVEICULOSContext(DbContextOptions<GERVEICULOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carro> Carros { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Configuracao> Configuracaos { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Modelo> Modelos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=GERVEICULOS;uid=root;pwd=sung87ju", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Carro>(entity =>
            {
                entity.ToTable("carro");

                entity.HasIndex(e => e.ClienteId, "fk_carro_cliente_idx");

                entity.HasIndex(e => e.Marca, "marca_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Modelo, "modelo_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Marca)
                    .HasMaxLength(45)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(45)
                    .HasColumnName("modelo");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Carros)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_carro_cliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.HasIndex(e => e.Email, "email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Telefone, "telefone_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Endereco)
                    .HasMaxLength(100)
                    .HasColumnName("endereco");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Telefone).HasColumnName("telefone");
            });

            modelBuilder.Entity<Configuracao>(entity =>
            {
                entity.ToTable("configuracao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiasDeLocacao).HasColumnName("diasDeLocacao");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marca");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.ToTable("modelo");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(45)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedido");

                entity.HasIndex(e => e.ClienteId, "fk_pedido_cliente1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carro)
                    .HasMaxLength(100)
                    .HasColumnName("carro");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.DataEntrega)
                    .HasColumnType("datetime")
                    .HasColumnName("dataEntrega");

                entity.Property(e => e.DataLocacao)
                    .HasColumnType("datetime")
                    .HasColumnName("dataLocacao");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pedido_cliente1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
