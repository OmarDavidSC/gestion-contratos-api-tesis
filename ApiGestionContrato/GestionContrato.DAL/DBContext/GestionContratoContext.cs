using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GestionContrato.Models;

namespace GestionContrato.DAL.DBContext
{
    public partial class GestionContratoContext : DbContext
    {
        public GestionContratoContext()
        {
        }

        public GestionContratoContext(DbContextOptions<GestionContratoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adenda> Adendas { get; set; } = null!;
        public virtual DbSet<AdministradoresContrato> AdministradoresContratos { get; set; } = null!;
        public virtual DbSet<ArchivoContrato> ArchivoContratos { get; set; } = null!;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Banco> Bancos { get; set; } = null!;
        public virtual DbSet<CompaniaAseguradora> CompaniaAseguradoras { get; set; } = null!;
        public virtual DbSet<Contrato> Contratos { get; set; } = null!;
        public virtual DbSet<DocumentosAdicionales> DocumentosAdicionales { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Garantia> Garantias { get; set; } = null!;
        public virtual DbSet<HistorialEvento> HistorialEventos { get; set; } = null!;
        public virtual DbSet<MetodoEntrega> MetodoEntregas { get; set; } = null!;
        public virtual DbSet<Moneda> Moneda { get; set; } = null!;
        public virtual DbSet<Poliza> Polizas { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<SistemaContratacion> SistemaContratacions { get; set; } = null!;
        public virtual DbSet<TipoAdenda> TipoAdenda { get; set; } = null!;
        public virtual DbSet<TipoContrato> TipoContratos { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; } = null!;
        public virtual DbSet<TipoGarantia> TipoGarantia { get; set; } = null!;
        public virtual DbSet<TipoPoliza> TipoPolizas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(local); DataBase=GestionContrato; Trusted_Connection=True; TrustServerCertificate=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adenda>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CodigoAdenda)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Monto)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Adenda)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adendas_Contrato");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Adenda)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adendas_Moneda");

                entity.HasOne(d => d.TipoAdenda)
                    .WithMany(p => p.Adenda)
                    .HasForeignKey(d => d.IdTipoAdenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adendas_TipoAdenda");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.AdendaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Adendas_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.AdendaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adendas_UsuarioRegistro");
            });

            modelBuilder.Entity<AdministradoresContrato>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.AdministradoresContratos)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdministradoresContratos_Contrato");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AdministradoresContratoIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdministradoresContratos_Usuario");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.AdministradoresContratoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_AdministradoresContratos_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.AdministradoresContratoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdministradoresContratos_UsuarioRegistro");
            });

            modelBuilder.Entity<ArchivoContrato>(entity =>
            {
                entity.ToTable("ArchivoContrato");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.ArchivoContratos)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArchivoContrato_Contrato");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.ArchivoContratoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_ArchivoContrato_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.ArchivoContratoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArchivoContrato_UsuarioRegistro");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.AreaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Area_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.AreaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_UsuarioRegistro");

                entity.HasOne(d => d.UsuarioResponsable)
                    .WithMany(p => p.AreaIdUsuarioResponsableNavigations)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_UsuarioResponsable");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.ToTable("Banco");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.BancoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Banco_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.BancoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Banco_UsuarioRegistro");
            });

            modelBuilder.Entity<CompaniaAseguradora>(entity =>
            {
                entity.ToTable("CompaniaAseguradora");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.CompaniaAseguradoraIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_CompaniaAseguradora_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.CompaniaAseguradoraIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompaniaAseguradora_UsuarioRegistro");
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("Contrato");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CodigoContrato)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComentarioCierreContrato).IsUnicode(false);

                entity.Property(e => e.DetalleContrato).IsUnicode(false);

                entity.Property(e => e.MontoContrato)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MontoTotal)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MotivoAnulacion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TituloContrato)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_Area");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_Estado");

                entity.HasOne(d => d.MetodoEntrega)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdMetodoEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_MetodoEntrega");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_Moneda");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_Proveedor");

                entity.HasOne(d => d.SistemaContratacion)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdSistemaContratacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_SistemaContratacion");

                entity.HasOne(d => d.TipoContrato)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdTipoContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_TipoContrato");

                entity.HasOne(d => d.UsuarioAprobadorCierre)
                    .WithMany(p => p.ContratoIdUsuarioAprobadorCierreNavigations)
                    .HasForeignKey(d => d.IdUsuarioAprobadorCierre)
                    .HasConstraintName("FK_Contrato_UsuarioAprobadorCierre");

                entity.HasOne(d => d.UsuarioAprobadorContrato)
                    .WithMany(p => p.ContratoIdUsuarioAprobadorContratoNavigations)
                    .HasForeignKey(d => d.IdUsuarioAprobadorContrato)
                    .HasConstraintName("FK_Contrato_UsuarioAprobadorContrato");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.ContratoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Contrato_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.ContratoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrato_UsuarioRegistro");
            });

            modelBuilder.Entity<DocumentosAdicionales>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.DocumentosAdicionales)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentosAdicionales_Contrato");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.DocumentosAdicionales)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentosAdicionales_TipoDocumento");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.DocumentosAdicionaleIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_DocumentosAdicionales_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.DocumentosAdicionaleIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentosAdicionales_UsuarioRegistro");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Garantia>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Monto)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroGarantia)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Banco)
                    .WithMany(p => p.Garantia)
                    .HasForeignKey(d => d.IdBanco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Garantias_Banco");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Garantia)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Garantias_Contrato");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Garantia)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Garantias_Moneda");

                entity.HasOne(d => d.TipoGarantia)
                    .WithMany(p => p.Garantia)
                    .HasForeignKey(d => d.IdTipoGarantia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Garantias_TipoGarantia");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.GarantiaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Garantias_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.GarantiaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Garantias_UsuarioRegistro");
            });

            modelBuilder.Entity<HistorialEvento>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.HistorialEventos)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistorialEventos_Contrato");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.HistorialEventos)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistorialEventos_UsuarioRegistro");
            });

            modelBuilder.Entity<MetodoEntrega>(entity =>
            {
                entity.ToTable("MetodoEntrega");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.MetodoEntregaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_MetodoEntrega_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.MetodoEntregaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetodoEntrega_UsuarioRegistro");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.MonedumIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Moneda_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.MonedumIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Moneda_UsuarioRegistro");
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Monto)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompaniaAseguradora)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdCompaniaAseguradora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polizas_CompaniaAseguradora");

                entity.HasOne(d => d.IdContratoNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polizas_Contrato");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polizas_Moneda");

                entity.HasOne(d => d.TipoPoliza)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdTipoPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polizas_TipoPoliza");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.PolizaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Polizas_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.PolizaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polizas_UsuarioRegistro");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("Proveedor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ruc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.ProveedorIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Proveedor_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.ProveedorIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proveedor_UsuarioRegistro");
            });

            modelBuilder.Entity<SistemaContratacion>(entity =>
            {
                entity.ToTable("SistemaContratacion");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.SistemaContratacionIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_SistemaContratacion_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.SistemaContratacionIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SistemaContratacion_UsuarioRegistro");
            });

            modelBuilder.Entity<TipoAdenda>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.TipoAdendumIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_TipoAdenda_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.TipoAdendumIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoAdenda_UsuarioRegistro");
            });

            modelBuilder.Entity<TipoContrato>(entity =>
            {
                entity.ToTable("TipoContrato");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.TipoContratoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_TipoContrato_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.TipoContratoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoContrato_UsuarioRegistro");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("TipoDocumento");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.TipoDocumentoIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_TipoDocumento_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.TipoDocumentoIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoDocumento_UsuarioRegistro");
            });

            modelBuilder.Entity<TipoGarantia>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.TipoGarantiumIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_TipoGarantia_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.TipoGarantiumIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoGarantia_UsuarioRegistro");
            });

            modelBuilder.Entity<TipoPoliza>(entity =>
            {
                entity.ToTable("TipoPoliza");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.TipoPolizaIdUsuarioModificacionNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_TipoPoliza_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.TipoPolizaIdUsuarioRegistroNavigations)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoPoliza_UsuarioRegistro");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(752)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(((([Nombre]+' ')+[ApellidoPaterno])+' ')+[ApellidoMaterno])", true);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Area");

                entity.HasOne(d => d.UsuarioModificacion)
                    .WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .HasConstraintName("FK_Usuario_UsuarioModificacion");

                entity.HasOne(d => d.UsuarioRegistro)
                    .WithMany(p => p.InverseIdUsuarioRegistroNavigation)
                    .HasForeignKey(d => d.IdUsuarioRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_UsuarioRegistro");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
