using AutoMapper;
using GestionContrato.Dto;
using GestionContrato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Utility
{
    public class AutoMapperModel: Profile
    {
        public AutoMapperModel()
        {

            #region Perfil
            CreateMap<Usuario, PerfilDto>()
               .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
               .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
               .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro))
               .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro));

            CreateMap<PerfilDto, Usuario>()
                .ForMember(dest => dest.FechaModificacion, opt => opt.MapFrom(src => src.FechaModificacion))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));
            #endregion

            //Adenda
            #region Adenda
            CreateMap<Adenda, AdendaDto>();
            CreateMap<AdendaDto, Adenda>();

            CreateMap<Adenda, AdendaDto>()
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
                .ForMember(dest => dest.TipoAdenda, opt => opt.MapFrom(src => src.TipoAdenda))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<AdendaDto, Adenda>()
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.Moneda.Id))
                .ForMember(dest => dest.IdTipoAdenda, opt => opt.MapFrom(src => src.TipoAdenda.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.Moneda, opt => opt.Ignore())
                .ForMember(dest => dest.TipoAdenda, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //AdministradoresContrato
            #region AdministradoresContrato
            CreateMap<AdministradoresContrato, AdministradoresContratoDto>();
            CreateMap<AdministradoresContratoDto, AdministradoresContrato>();

            CreateMap<AdministradoresContrato, AdministradoresContratoDto>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<AdministradoresContratoDto, AdministradoresContrato>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Usuario.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());

            #endregion

            // ArchivoContrato
            #region ArchivoContrato
            CreateMap<ArchivoContrato, ArchivoContratoDto>();
            CreateMap<ArchivoContratoDto, ArchivoContrato>();

            CreateMap<ArchivoContrato, ArchivoContratoDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<ArchivoContratoDto, ArchivoContrato>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Area 
            #region Area
            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();

            CreateMap<Area, AreaDto>()
                .ForMember(dest => dest.UsuarioResponsable, opt => opt.MapFrom(src => src.UsuarioResponsable))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<AreaDto, Area>()
                .ForMember(dest => dest.IdUsuarioResponsable, opt => opt.MapFrom(src => src.UsuarioResponsable.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.UsuarioResponsable, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Banco
            #region Banco
            CreateMap<Banco, BancoDto>();
            CreateMap<BancoDto, Banco>();

            CreateMap<Banco, BancoDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                 .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<BancoDto, Banco>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //ComapniaAseguradora
            #region ComapniaAseguradora
            CreateMap<CompaniaAseguradora, CompaniaAseguradoraDto>();
            CreateMap<CompaniaAseguradoraDto, CompaniaAseguradora>();

            CreateMap<CompaniaAseguradora, CompaniaAseguradoraDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                 .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<CompaniaAseguradoraDto, CompaniaAseguradora>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Contrato
            #region Contrato
            CreateMap<Contrato, ContratoDto>();
            CreateMap<ContratoDto, Contrato>();

            CreateMap<Contrato, ContratoDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Proveedor, opt => opt.MapFrom(src => src.Proveedor))
                .ForMember(dest => dest.TipoContrato, opt => opt.MapFrom(src => src.TipoContrato))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
                .ForMember(dest => dest.MetodoEntrega, opt => opt.MapFrom(src => src.MetodoEntrega))
                .ForMember(dest => dest.SistemaContratacion, opt => opt.MapFrom(src => src.SistemaContratacion))
                .ForMember(dest => dest.UsuarioAprobadorContrato, opt => opt.MapFrom(src => src.UsuarioAprobadorContrato))
                .ForMember(dest => dest.UsuarioAprobadorCierre, opt => opt.MapFrom(src => src.UsuarioAprobadorCierre))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion))
                .ForMember(dest => dest.Adenda, opt => opt.MapFrom(src => src.Adenda))
                .ForMember(dest => dest.AdministradoresContratos, opt => opt.MapFrom(src => src.AdministradoresContratos))
                .ForMember(dest => dest.ArchivoContratos, opt => opt.MapFrom(src => src.ArchivoContratos))
                .ForMember(dest => dest.DocumentosAdicionales, opt => opt.MapFrom(src => src.DocumentosAdicionales))
                .ForMember(dest => dest.Garantia, opt => opt.MapFrom(src => src.Garantia))
                .ForMember(dest => dest.Polizas, opt => opt.MapFrom(src => src.Polizas))
                .ForMember(dest => dest.HistorialEventos, opt => opt.MapFrom(src => src.HistorialEventos));


            CreateMap<ContratoDto, Contrato>()
                .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.Estado.Id))
                .ForMember(dest => dest.IdArea, opt => opt.MapFrom(src => src.Area.Id))
                .ForMember(dest => dest.IdProveedor, opt => opt.MapFrom(src => src.Proveedor.Id))
                .ForMember(dest => dest.IdTipoContrato, opt => opt.MapFrom(src => src.TipoContrato.Id))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.Moneda.Id))
                .ForMember(dest => dest.IdMetodoEntrega, opt => opt.MapFrom(src => src.MetodoEntrega.Id))
                .ForMember(dest => dest.IdSistemaContratacion, opt => opt.MapFrom(src => src.SistemaContratacion.Id))
                .ForMember(dest => dest.IdUsuarioAprobadorContrato, opt => opt.MapFrom(src => src.UsuarioAprobadorContrato.Id))
                .ForMember(dest => dest.IdUsuarioAprobadorCierre, opt => opt.MapFrom(src => src.UsuarioAprobadorCierre.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))


                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.Area, opt => opt.Ignore())
                .ForMember(dest => dest.Proveedor, opt => opt.Ignore())
                .ForMember(dest => dest.TipoContrato, opt => opt.Ignore())
                .ForMember(dest => dest.Moneda, opt => opt.Ignore())
                .ForMember(dest => dest.MetodoEntrega, opt => opt.Ignore())
                .ForMember(dest => dest.SistemaContratacion, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioAprobadorContrato, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioAprobadorCierre, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());

            #endregion

            //DocumentosAdicionales
            #region DocumentosAdicionales
            CreateMap<DocumentosAdicionales, DocumentosAdicionalesDto>();
            CreateMap<DocumentosAdicionalesDto, DocumentosAdicionales>();

            CreateMap<DocumentosAdicionales, DocumentosAdicionalesDto>()
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<DocumentosAdicionalesDto, DocumentosAdicionales>()
                .ForMember(dest => dest.IdTipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.TipoDocumento, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Estado
            #region Estado
            CreateMap<Estado, EstadoDto>();
            CreateMap<EstadoDto, Estado>();
            #endregion

            //Garantia
            #region Garantia
            CreateMap<Garantia, GarantiaDto>();
            CreateMap<GarantiaDto, Garantia>();

            CreateMap<Garantia, GarantiaDto>()
                .ForMember(dest => dest.Banco, opt => opt.MapFrom(src => src.Banco))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
                .ForMember(dest => dest.TipoGarantia, opt => opt.MapFrom(src => src.TipoGarantia))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<GarantiaDto, Garantia>()
                .ForMember(dest => dest.IdBanco, opt => opt.MapFrom(src => src.Banco.Id))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.Moneda.Id))
                .ForMember(dest => dest.IdTipoGarantia, opt => opt.MapFrom(src => src.TipoGarantia.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.Banco, opt => opt.Ignore())
                .ForMember(dest => dest.Moneda, opt => opt.Ignore())
                .ForMember(dest => dest.TipoGarantia, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //HistorialEvento
            #region HistorialEvento
            CreateMap<HistorialEvento, HistorialEventoDto>();
            CreateMap<HistorialEventoDto, HistorialEvento>();

            CreateMap<HistorialEvento, HistorialEventoDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro));

            CreateMap<HistorialEventoDto, HistorialEvento>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))

                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore());
            #endregion

            //MetodoEntrega 
            #region MetodoEntrega
            CreateMap<MetodoEntrega, MetodoEntregaDto>();
            CreateMap<MetodoEntregaDto, MetodoEntrega>();

            CreateMap<MetodoEntrega, MetodoEntregaDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<MetodoEntregaDto, MetodoEntrega>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Moneda
            #region Moneda
            CreateMap<Moneda, MonedaDto>();
            CreateMap<MonedaDto, Moneda>();

            CreateMap<Moneda, MonedaDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<MonedaDto, Moneda>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Poliza 
            #region Poliza
            CreateMap<Poliza, PolizaDto>();
            CreateMap<PolizaDto, Poliza>();

            CreateMap<Poliza, PolizaDto>()
                .ForMember(dest => dest.CompaniaAseguradora, opt => opt.MapFrom(src => src.CompaniaAseguradora))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.Moneda))
                .ForMember(dest => dest.TipoPoliza, opt => opt.MapFrom(src => src.TipoPoliza))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<PolizaDto, Poliza>()
                .ForMember(dest => dest.IdCompaniaAseguradora, opt => opt.MapFrom(src => src.CompaniaAseguradora.Id))
                .ForMember(dest => dest.IdMoneda, opt => opt.MapFrom(src => src.Moneda.Id))
                .ForMember(dest => dest.IdTipoPoliza, opt => opt.MapFrom(src => src.TipoPoliza.Id))
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))

                .ForMember(dest => dest.CompaniaAseguradora, opt => opt.Ignore())
                .ForMember(dest => dest.Moneda, opt => opt.Ignore())
                .ForMember(dest => dest.TipoPoliza, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Proveedor
            #region Proveedor
            CreateMap<Proveedor, ProveedorDto>();
            CreateMap<ProveedorDto, Proveedor>();

            CreateMap<Proveedor, ProveedorDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<ProveedorDto, Proveedor>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //SistemaContratacion
            #region SistemaContratacion
            CreateMap<SistemaContratacion, SistemaContratacionDto>();
            CreateMap<SistemaContratacionDto, SistemaContratacion>();

            CreateMap<SistemaContratacion, SistemaContratacionDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<SistemaContratacionDto, SistemaContratacion>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //TipoAdenda
            #region  TipoAdenda
            CreateMap<TipoAdenda, TipoAdendaDto>();
            CreateMap<TipoAdendaDto, TipoAdenda>();

            CreateMap<TipoAdenda, TipoAdendaDto>()
                    .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                    .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<TipoAdendaDto, TipoAdenda>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());

            #endregion

            //TipoContrato
            #region TipoContrato
            CreateMap<TipoContrato, TipoContratoDto>();
            CreateMap<TipoContratoDto, TipoContrato>();

            CreateMap<TipoContrato, TipoContratoDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<TipoContratoDto, TipoContrato>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //TipoDocumento
            #region TipoDocumento
            CreateMap<TipoDocumento, TipoDocumentoDto>();
            CreateMap<TipoDocumentoDto, TipoDocumento>();

            CreateMap<TipoDocumento, TipoDocumentoDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<TipoDocumentoDto, TipoDocumento>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //TipoGarantia
            #region  TipoGarantia
            CreateMap<TipoGarantia, TipoGarantiaDto>();
            CreateMap<TipoGarantiaDto, TipoGarantia>();

            CreateMap<TipoGarantia, TipoGarantiaDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<TipoGarantiaDto, TipoGarantia>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //TipoPoliza
            #region TipoPoliza
            CreateMap<TipoPoliza, TipoPolizaDto>();
            CreateMap<TipoPolizaDto, TipoPoliza>();

            CreateMap<TipoPoliza, TipoPolizaDto>()
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<TipoPolizaDto, TipoPoliza>()
                .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
                .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
                .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());
            #endregion

            //Usuario
            #region Usuario
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<Usuario, UsuarioDto>()
             .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
             .ForMember(dest => dest.UsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro))
             .ForMember(dest => dest.UsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion));

            CreateMap<UsuarioDto, Usuario>()
            .ForMember(dest => dest.IdArea, opt => opt.MapFrom(src => src.Area.Id))
            .ForMember(dest => dest.IdUsuarioRegistro, opt => opt.MapFrom(src => src.UsuarioRegistro.Id))
            .ForMember(dest => dest.IdUsuarioModificacion, opt => opt.MapFrom(src => src.UsuarioModificacion.Id))
            .ForMember(dest => dest.Area, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioRegistro, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioModificacion, opt => opt.Ignore());

            #endregion
        }
    }
}
