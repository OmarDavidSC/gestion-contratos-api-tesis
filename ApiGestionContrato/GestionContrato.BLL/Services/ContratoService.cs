using AutoMapper;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestionContrato.BLL.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IGenericRepository<Contrato> contratoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        private readonly IGenericRepository<Adenda> adendaRepository;
        private readonly IGenericRepository<AdministradoresContrato> administradoresContratoRepository;
        private readonly IGenericRepository<ArchivoContrato> archivoContratoRepository;
        private readonly IGenericRepository<DocumentosAdicionales> documentoAdicionalesRepository;
        private readonly IGenericRepository<Garantia> garantiaRepository;
        private readonly IGenericRepository<HistorialEvento> historialEventoRepository;
        private readonly IGenericRepository<Poliza> polizaRepository;

        public ContratoService
        (
         IGenericRepository<Contrato> _contratoRepository,
         IUnitOfWork _unitOfWork,
         IMapper _mapper,
         IGenericRepository<Adenda> _adendaRepository,
         IGenericRepository<AdministradoresContrato> _administradoresContratoRepository,
         IGenericRepository<ArchivoContrato> _archivoContratoRepository,
         IGenericRepository<DocumentosAdicionales> _documentoAdicionalesRepository,
         IGenericRepository<Garantia> _garantiaRepository,
         IGenericRepository<HistorialEvento> _historialEventoRepository,
         IGenericRepository<Poliza> _polizaRepository
        )
        {
            contratoRepository = _contratoRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            adendaRepository = _adendaRepository;
            administradoresContratoRepository = _administradoresContratoRepository;
            archivoContratoRepository = _archivoContratoRepository;
            documentoAdicionalesRepository = _documentoAdicionalesRepository;
            garantiaRepository = _garantiaRepository;
            historialEventoRepository = _historialEventoRepository;
            polizaRepository = _polizaRepository;
        }

        public async Task<List<ContratoDto>> listaBandeja(FiltroBandejaDto? filtroBandeja)
        {
            try
            {

                Guid IdEnRegistro = new Guid("FD4668DA-0F09-4810-A13E-9E85B50693EA");
                Guid IdEnAprobacion = new Guid("51A3BEB9-0D7F-4147-9E24-6604E7E682D5");
                Guid IdVigente = new Guid("3155E7A5-ADC1-4D29-BA81-83FD95DD7ED5");
                Guid IdVencido = new Guid("72301DAC-FDF1-473C-9F6A-B6192C7F67D5");
                Guid IdCerrado = new Guid("B621A222-C8D5-4CBC-B842-F887D388E9DC");
                Guid IdAnulado = new Guid("4000E95B-8EE3-441E-9C61-6EA3F6183401");
                Guid IdRechazado = new Guid("3666655A-47EE-478B-B6CB-CC9A557E0CFF");
                Guid IdObservado = new Guid("6BC34E35-A223-4FE7-BA8D-E87F2784A83F");

                var queryContrato = await contratoRepository.QuerySql();

                if (filtroBandeja != null)
                {
                    if (filtroBandeja.Vista == "Pendientes")
                    {
                        queryContrato = queryContrato.Where(x =>
                            (x.Estado.Id == IdEnRegistro && x.IdUsuarioRegistro == filtroBandeja.IdUsuarioRegistro) ||
                            (x.Estado.Id == IdEnAprobacion && x.IdUsuarioAprobadorContrato == filtroBandeja.IdUsuarioRegistro) ||
                            (x.Estado.Id == IdVigente && x.IdUsuarioRegistro == filtroBandeja.IdUsuarioRegistro) ||
                            (x.Estado.Id == IdObservado && x.IdUsuarioRegistro == filtroBandeja.IdUsuarioRegistro)
                        );

                    }

                    if (filtroBandeja.Vista == "Administrativa")
                    {
                        //contratos donde el usuario es administrador
                        var queryAdmin = await administradoresContratoRepository.QuerySql();
                        var listaIdContratosAdmin = queryAdmin
                            .Where(x => x.IdUsuario == filtroBandeja.IdUsuarioRegistro)
                            .Select(x => x.IdContrato)
                            .ToList();

                        queryContrato = queryContrato.Where(x => listaIdContratosAdmin.Contains(x.Id));
                    }

                    if (filtroBandeja.FechaRegistroInicio.HasValue && filtroBandeja.FechaRegistroFin.HasValue)
                    {
                        DateTime FechaRegistroInicio = new DateTime(filtroBandeja.FechaRegistroInicio.Value.Year, filtroBandeja.FechaRegistroInicio.Value.Month, filtroBandeja.FechaRegistroInicio.Value.Day);
                        DateTime FechaRegistroFin = new DateTime(filtroBandeja.FechaRegistroFin.Value.Year, filtroBandeja.FechaRegistroFin.Value.Month, filtroBandeja.FechaRegistroFin.Value.Day);
                        FechaRegistroFin = FechaRegistroFin.AddHours(23).AddMinutes(59);

                        queryContrato = queryContrato.Where(x =>
                        x.FechaRegistro >= FechaRegistroInicio &&
                        x.FechaRegistro <= FechaRegistroFin);
                    }

                    if (filtroBandeja.ListaTipoContrato.Count > 0)
                    {
                        queryContrato = queryContrato.Where(x => filtroBandeja.ListaTipoContrato.Contains(x.IdTipoContrato.Value));
                    }

                    if (filtroBandeja.ListaEstado.Count > 0)
                    {
                        queryContrato = queryContrato.Where(x => filtroBandeja.ListaEstado.Contains(x.IdEstado.Value));
                    }

                    if (filtroBandeja.ListaArea.Count > 0)
                    {
                        queryContrato = queryContrato.Where(x =>
                        (x.IdArea != null && filtroBandeja.ListaArea.Contains(x.IdArea.Value)));
                    }

                    if (!string.IsNullOrEmpty(filtroBandeja.TextoBusquedaRapida))
                    {
                        queryContrato = queryContrato.Where(x => x.CodigoContrato.Contains(filtroBandeja.TextoBusquedaRapida) || x.DetalleContrato.Contains(filtroBandeja.TextoBusquedaRapida));
                    }
                }

                var contratos = queryContrato
                        .Include(x => x.Estado)
                        .Include(x => x.Area)
                        .Include(x => x.Proveedor)
                        .Include(x => x.TipoContrato)
                        .Include(x => x.SistemaContratacion)
                        .OrderByDescending(x => x.FechaRegistro)
                        .Select(x => new ContratoDto
                        {
                            Id = x.Id,
                            CodigoContrato = x.CodigoContrato,
                            TituloContrato = x.TituloContrato,
                            DetalleContrato = x.DetalleContrato,
                            FechaInicio = x.FechaInicio,
                            FechaFin = x.FechaFin,
                            MontoContrato = x.MontoContrato,
                            MontoTotal = x.MontoTotal,
                            FechaRegistro = x.FechaRegistro,
                            FechaModificacion = x.FechaModificacion,
                            FechaFinReal = x.FechaFinReal,
                            DiasFaltanParaVencimiento = x.FechaFinReal.HasValue ? (x.FechaFinReal.Value - DateTime.Now).Days : (int?)null,
                            Estado = new EstadoDto { Id = x.Estado.Id, Nombre = x.Estado.Nombre },
                            Moneda = new MonedaDto { Id = x.Moneda.Id, Nombre = x.Moneda.Nombre },
                            MetodoEntrega = new MetodoEntregaDto { Id = x.MetodoEntrega.Id, Nombre = x.MetodoEntrega.Nombre },
                            Area = x.Area != null ? new AreaDto { Id = x.Area.Id, Nombre = x.Area.Nombre } : null,
                            Proveedor = x.Proveedor != null ? new ProveedorDto { Id = x.Proveedor.Id, Nombre = x.Proveedor.Nombre } : null,
                            TipoContrato = x.TipoContrato != null ? new TipoContratoDto { Id = x.TipoContrato.Id, Nombre = x.TipoContrato.Nombre } : null,
                            SistemaContratacion = x.SistemaContratacion != null ? new SistemaContratacionDto { Id = x.SistemaContratacion.Id, Nombre = x.SistemaContratacion.Nombre } : null
                        })
                        .ToList();

                //var ListaContrato = mapper.Map<List<ContratoDto>>(contratos);

                DateTime fechaActual = DateTime.Now;
                foreach (var item in contratos)
                {
                    if (item.FechaFinReal.HasValue)
                    {
                        item.DiasFaltanParaVencimiento = (item.FechaFinReal.Value - fechaActual).Days;
                    }
                    else
                    {
                        item.DiasFaltanParaVencimiento = null;
                    }
                }

                var queryAdministradores = await administradoresContratoRepository.QuerySql();
                var listaAdmin = queryAdministradores
                    .Include(x => x.Usuario)
                    .Select(x => new AdministradoresContratoDto
                    {
                        Id = x.Id,
                        IdContrato = x.IdContrato,
                        Usuario = new UsuarioDto
                        {
                            Id = x.Usuario.Id,
                            NombreCompleto = x.Usuario.NombreCompleto,
                            Correo = x.Usuario.Correo,
                            Rol = x.Usuario.Rol,
                            Habilitado = x.Usuario.Habilitado
                        }
                    })
                    .ToList();

                var ListaAdministradores = mapper.Map<List<AdministradoresContratoDto>>(listaAdmin);

                var queryArchivos = await archivoContratoRepository.QuerySql();
                var listaArchivos = queryArchivos.OrderBy(x => x.FechaRegistro).ToList();

                var ListaArchivos = mapper.Map<List<ArchivoContratoDto>>(listaArchivos); ;

                foreach (var item in contratos)
                {
                    item.AdministradoresContratos = ListaAdministradores.Where(x => x.IdContrato == item.Id).ToList();
                    item.ArchivoContratos = ListaArchivos.Where(x => x.IdContrato == item.Id).ToList();
                }

                return contratos;


            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<ContratoDto> getIdContrato(Guid id)
        {
            try
            {

                var queryContrato = await contratoRepository.QuerySql(c => c.Id == id);
                var contrato = queryContrato
                                    .Include(x => x.Estado)
                                    .Include(x => x.Area)
                                    .Include(x => x.Proveedor)
                                    .Include(x => x.TipoContrato)
                                    .Include(x => x.Moneda)
                                    .Include(x => x.MetodoEntrega)
                                    .Include(x => x.SistemaContratacion)
                                    .Include(x => x.UsuarioAprobadorContrato)
                                    .Include(x => x.UsuarioAprobadorCierre)
                                    .Include(x => x.UsuarioRegistro)
                                    .Include(x => x.UsuarioModificacion)
                                    .FirstOrDefault();

                if (contrato == null)
                {
                    throw new TaskCanceledException("Contrato no encontrado");
                }

                var queryAdendas = await adendaRepository.QuerySql(c => c.IdContrato == id);

                var listaAdendas = queryAdendas.Include(x => x.TipoAdenda).Include(x => x.Moneda).OrderBy(x => x.CodigoAdenda).ToList();
                contrato.Adenda = listaAdendas;

                var queryAdministradores = await administradoresContratoRepository.QuerySql(c => c.IdContrato == id);
                var listaAdmin = queryAdministradores.Include(x => x.Usuario).ToList();

                contrato.AdministradoresContratos = listaAdmin;

                var queryArchivos = await archivoContratoRepository.QuerySql(c => c.IdContrato == id);
                var listaArchivos = queryArchivos.OrderBy(x => x.FechaRegistro).ToList();

                contrato.ArchivoContratos = listaArchivos;

                var queryDocAdicionales = await documentoAdicionalesRepository.QuerySql(c => c.IdContrato == id);
                var listaDocAdicionales = queryDocAdicionales.Include(x => x.TipoDocumento).OrderBy(x => x.FechaRegistro).ToList();

                contrato.DocumentosAdicionales = listaDocAdicionales;

                var queryGarantia = await garantiaRepository.QuerySql(c => c.IdContrato == id);
                var listaGarantia = queryGarantia
                                        .Include(x => x.Banco)
                                        .Include(x => x.TipoGarantia)
                                        .Include(x => x.Moneda).ToList();

                contrato.Garantia = listaGarantia;

                var queryHistorial = await historialEventoRepository.QuerySql(c => c.IdContrato == id);
                var listaHistorial = queryHistorial.Include(x => x.UsuarioRegistro).OrderByDescending(x => x.FechaRegistro).ToList();

                contrato.HistorialEventos = listaHistorial;

                var queryPoliza = await polizaRepository.QuerySql(x => x.IdContrato == id);
                var listaPolizas = queryPoliza
                                        .Include(x => x.CompaniaAseguradora)
                                        .Include(x => x.TipoPoliza)
                                        .Include(x => x.Moneda)
                                        .OrderByDescending(x => x.FechaRegistro).ToList();

                contrato.Polizas = listaPolizas;

                return mapper.Map<ContratoDto>(contrato);


            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<ContratoDto> registrarContrato(ContratoDto modelo)
        {
            try
            {
                ContratoDto result = new ContratoDto();

                var ContratoEntidad = mapper.Map<Contrato>(modelo);
                ContratoEntidad.Adenda = new List<Adenda>();
                ContratoEntidad.AdministradoresContratos = new List<AdministradoresContrato>();
                ContratoEntidad.ArchivoContratos = new List<ArchivoContrato>();
                ContratoEntidad.DocumentosAdicionales = new List<DocumentosAdicionales>();
                ContratoEntidad.Garantia = new List<Garantia>();
                ContratoEntidad.HistorialEventos = new List<HistorialEvento>();
                ContratoEntidad.Polizas = new List<Poliza>();

                var administradoresContratoEntidad = mapper.Map<List<AdministradoresContrato>>(modelo.AdministradoresContratos);
                var archivoContratoEntidad = mapper.Map<List<ArchivoContrato>>(modelo.ArchivoContratos);
                var documentoAdicionalesEntidad = mapper.Map<List<DocumentosAdicionales>>(modelo.DocumentosAdicionales);
                var hIstorialEntidad = mapper.Map<List<HistorialEvento>>(modelo.HistorialEventos);

                int anioActual = int.Parse(string.Format("{0:yy}", DateTime.Now));

                using (var scope = await unitOfWork.BeginTransactionAsync())
                {
                    #region correlativo
                    var anio = new SqlParameter
                    {
                        ParameterName = "@i_anio",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = anioActual
                    };

                    var tipo = new SqlParameter
                    {
                        ParameterName = "@v_tipo",
                        DbType = System.Data.DbType.String,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = "C",
                        Size = 100
                    };

                    var longitud = new SqlParameter
                    {
                        ParameterName = "@i_longitud",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = 5
                    };

                    var codigo = new SqlParameter
                    {
                        ParameterName = "@v_codigo_generado",
                        DbType = System.Data.DbType.String,
                        Direction = System.Data.ParameterDirection.Output,
                        Size = 100
                    };

                    await unitOfWork.ExecuteProcedure("[dbo].[Proc_GenerarCorrelativoEmpresa]", anio, tipo, longitud, codigo);

                    string codigoGenerado = "C" + "-" + anioActual + "-" + codigo.Value.ToString();
                    #endregion

                    ContratoEntidad.CodigoContrato = codigoGenerado;
                    ContratoEntidad.FechaRegistro = DateTime.Now;

                    await contratoRepository.AddAsync(ContratoEntidad);
                    await unitOfWork.SaveChangesAsync();

                    foreach (var administradorContrato in administradoresContratoEntidad)
                    {
                        administradorContrato.Crear(ContratoEntidad.Id, ContratoEntidad.IdUsuarioRegistro);
                        await administradoresContratoRepository.CreateAsync(administradorContrato);
                    }

                    foreach (var archivoContrato in archivoContratoEntidad)
                    {
                        archivoContrato.Crear(ContratoEntidad.Id, ContratoEntidad.IdUsuarioRegistro);
                        await archivoContratoRepository.CreateAsync(archivoContrato);
                    }

                    foreach (var documentoAdicional in documentoAdicionalesEntidad)
                    {
                        documentoAdicional.Crear(ContratoEntidad.Id, ContratoEntidad.IdUsuarioRegistro);
                        await documentoAdicionalesRepository.CreateAsync(documentoAdicional);
                    }

                    foreach (var historial in hIstorialEntidad)
                    {
                        historial.Crear(ContratoEntidad.Id, ContratoEntidad.IdUsuarioRegistro);
                        await historialEventoRepository.CreateAsync(historial);
                    }

                    await unitOfWork.SaveChangesAsync();
                    await scope.CommitAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<ContratoDto> editarContraro(ContratoDto modelo)
        {
            try
            {
                ContratoDto result = new ContratoDto();

                var archivosActuales = await archivoContratoRepository.GetAllAsync(a => a.IdContrato == modelo.Id);
                var nuevosArchivosContratoEntidad = mapper.Map<List<ArchivoContrato>>(modelo.ArchivoContratos);

                var documentosActuales = await documentoAdicionalesRepository.GetAllAsync(d => d.IdContrato == modelo.Id);
                var nuevosDocumentosContratosEntidad = modelo.DocumentosAdicionales;// mapper.Map<List<DocumentosAdicionales>>(modelo.DocumentosAdicionales);

                var administradoresActuales = await administradoresContratoRepository.GetAllAsync(a => a.IdContrato == modelo.Id);
                var nuevosAdministradoresContratoEntidad = mapper.Map<List<AdministradoresContrato>>(modelo.AdministradoresContratos);

                var historialContratoEntidad = mapper.Map<List<HistorialEvento>>(modelo.HistorialEventos);

                using (var scope = await unitOfWork.BeginTransactionAsync())
                {
                    var contratoEntidad = await contratoRepository.Get(c => c.Id == modelo.Id);
                    if (contratoEntidad == null)
                    {
                        throw new Exception("Contrato no encontrado");
                    }

                    contratoEntidad.IdEstado = modelo.Estado.Id.Value;
                    contratoEntidad.IdUsuarioModificacion = modelo.UsuarioModificacion.Id;
                    contratoEntidad.FechaModificacion = DateTime.Now;

                    contratoEntidad.TituloContrato = modelo.TituloContrato;
                    contratoEntidad.IdArea = modelo.Area.Id.Value;
                    contratoEntidad.IdProveedor = modelo.Proveedor.Id.Value;
                    contratoEntidad.IdTipoContrato = modelo.TipoContrato.Id.Value;
                    contratoEntidad.DetalleContrato = modelo.DetalleContrato;
                    contratoEntidad.FechaInicio = modelo.FechaInicio;
                    contratoEntidad.FechaFin = modelo.FechaFin;
                    contratoEntidad.FechaFinReal = modelo.FechaFin;
                    contratoEntidad.MontoContrato = modelo.MontoContrato;
                    contratoEntidad.IdMoneda = modelo.Moneda.Id.Value;
                    contratoEntidad.IdMetodoEntrega = modelo.MetodoEntrega.Id.Value;
                    contratoEntidad.IdSistemaContratacion = modelo.SistemaContratacion.Id.Value;
                    contratoEntidad.IdUsuarioAprobadorContrato = modelo.UsuarioAprobadorContrato.Id;

                    await contratoRepository.UpdateAsync(contratoEntidad);

                    foreach (var archivo in nuevosArchivosContratoEntidad)
                    {
                        var archivoExistente = archivosActuales.FirstOrDefault(a => a.Id == archivo.Id);
                        if (archivoExistente != null)
                        {
                            if (archivo.Eliminado)
                            {
                                await archivoContratoRepository.DeleteAsync(archivoExistente);
                            }
                            else
                            {
                                archivoExistente.NombreArchivo = archivo.NombreArchivo;
                                await archivoContratoRepository.UpdateAsync(archivoExistente);
                            }
                        }
                        else
                        {
                            archivo.Crear(contratoEntidad.Id, modelo.UsuarioModificacion.Id.Value);
                            await archivoContratoRepository.AddAsync(archivo);
                        }
                    }

                    foreach (var documento in nuevosDocumentosContratosEntidad)
                    {
                        var documentoExistente = documentosActuales.FirstOrDefault(d => d.Id == documento.Id);
                        if (documentoExistente != null)
                        {
                            if (documento.Eliminado.Value)
                            {
                                await documentoAdicionalesRepository.DeleteAsync(documentoExistente);
                            }
                            else
                            {
                                documentoExistente.NombreArchivo = documento.NombreArchivo;
                                documentoExistente.IdTipoDocumento = documento.TipoDocumento.Id.Value;
                                await documentoAdicionalesRepository.UpdateAsync(documentoExistente);
                            }
                        }
                        else
                        {
                            DocumentosAdicionales documentoAdicional = new DocumentosAdicionales();
                            documentoAdicional.NombreArchivo = documento.NombreArchivo;
                            documentoAdicional.ByteArchivo = documento.ByteArchivo;
                            documentoAdicional.IdTipoDocumento = documento.TipoDocumento.Id.Value;
                            documentoAdicional.Crear(contratoEntidad.Id, modelo.UsuarioModificacion.Id.Value);
                            await documentoAdicionalesRepository.AddAsync(documentoAdicional);
                        }
                    }

                    foreach (var administrador in administradoresActuales)
                    {
                        await administradoresContratoRepository.DeleteAsync(administrador);
                    }

                    foreach (var administrador in nuevosAdministradoresContratoEntidad)
                    {
                        administrador.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioRegistro);
                        await administradoresContratoRepository.AddAsync(administrador);
                    }

                    foreach (var historial in historialContratoEntidad)
                    {
                        historial.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioModificacion.Value);
                        await historialEventoRepository.AddAsync(historial);
                    }

                    await unitOfWork.SaveChangesAsync();
                    await scope.CommitAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<ContratoDto> guardarContrato(ContratoDto modelo)
        {
            try
            {
                ContratoDto result = new ContratoDto();

                var garantiasActuales = await garantiaRepository.GetAllAsync(g => g.IdContrato == modelo.Id);
                var nuevasGarantiasContratoEntidad = mapper.Map<List<Garantia>>(modelo.Garantia);

                var polizasActuales = await polizaRepository.GetAllAsync(p => p.IdContrato == modelo.Id);
                var nuevasPolizasContratoEntidad = mapper.Map<List<Poliza>>(modelo.Polizas);

                var historialContratoEntidad = mapper.Map<List<HistorialEvento>>(modelo.HistorialEventos);

                var AdendasActuales = await adendaRepository.GetAllAsync(a => a.IdContrato == modelo.Id);
                var nuevasAdendasContratoEntidad = mapper.Map<List<Adenda>>(modelo.Adenda);

                var administradoresActuales = await administradoresContratoRepository.GetAllAsync(a => a.IdContrato == modelo.Id);
                var nuevosAdministradoresContratoEntidad = mapper.Map<List<AdministradoresContrato>>(modelo.AdministradoresContratos);

                var documentosActuales = await documentoAdicionalesRepository.GetAllAsync(d => d.IdContrato == modelo.Id);
                var nuevosDocumentosContratosEntidad = modelo.DocumentosAdicionales;

                using (var scope = await unitOfWork.BeginTransactionAsync())
                {
                    var contratoEntidad = await contratoRepository.Get(c => c.Id == modelo.Id);
                    if (contratoEntidad == null)
                    {
                        throw new Exception("Contrato no encontrado");
                    }


                    contratoEntidad.FechaFinReal = modelo.FechaFinReal;
                    contratoEntidad.MontoTotal = modelo.MontoTotal;
                    contratoEntidad.IdUsuarioModificacion = modelo.UsuarioModificacion.Id;
                    contratoEntidad.FechaModificacion = DateTime.Now;

                    foreach (var administrador in administradoresActuales)
                    {
                        await administradoresContratoRepository.DeleteAsync(administrador);
                    }

                    foreach (var administrador in nuevosAdministradoresContratoEntidad)
                    {
                        administrador.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioRegistro);
                        await administradoresContratoRepository.AddAsync(administrador);
                    }

                    foreach (var documento in nuevosDocumentosContratosEntidad)
                    {
                        var documentoExistente = documentosActuales.FirstOrDefault(d => d.Id == documento.Id);
                        if (documentoExistente != null)
                        {
                            if (documento.Eliminado.Value)
                            {
                                await documentoAdicionalesRepository.DeleteAsync(documentoExistente);
                            }
                            else
                            {
                                documentoExistente.NombreArchivo = documento.NombreArchivo;
                                documentoExistente.IdTipoDocumento = documento.TipoDocumento.Id.Value;
                                await documentoAdicionalesRepository.UpdateAsync(documentoExistente);
                            }
                        }
                        else
                        {
                            DocumentosAdicionales documentoAdicional = new DocumentosAdicionales();
                            documentoAdicional.NombreArchivo = documento.NombreArchivo;
                            documentoAdicional.ByteArchivo = documento.ByteArchivo;
                            documentoAdicional.IdTipoDocumento = documento.TipoDocumento.Id.Value;
                            documentoAdicional.Crear(contratoEntidad.Id, modelo.UsuarioModificacion.Id.Value);
                            await documentoAdicionalesRepository.AddAsync(documentoAdicional);
                        }
                    }

                    foreach (var garantia in nuevasGarantiasContratoEntidad)
                    {
                        var garantiaExistente = garantiasActuales.FirstOrDefault(g => g.Id == garantia.Id);
                        if (garantiaExistente != null)
                        {
                            if (garantia.Eliminado)
                            {
                                await garantiaRepository.DeleteAsync(garantiaExistente);
                            }
                            else
                            {
                                garantiaExistente.NumeroGarantia = garantia.NumeroGarantia;
                                garantiaExistente.IdBanco = garantia.IdBanco;
                                garantiaExistente.IdTipoGarantia = garantia.IdTipoGarantia;
                                garantiaExistente.Monto = garantia.Monto;
                                garantiaExistente.IdMoneda = garantia.IdMoneda;
                                garantiaExistente.FechaInicio = garantia.FechaInicio;
                                garantiaExistente.FechaFin = garantia.FechaFin;
                                garantiaExistente.NombreArchivo = garantia.NombreArchivo;
                                await garantiaRepository.UpdateAsync(garantiaExistente);
                            }
                        }
                        else
                        {
                            garantia.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioRegistro);
                            await garantiaRepository.AddAsync(garantia);
                        }
                    }

                    foreach (var poliza in nuevasPolizasContratoEntidad)
                    {
                        var polizaExistente = polizasActuales.FirstOrDefault(p => p.Id == poliza.Id);
                        if (polizaExistente != null)
                        {
                            if (poliza.Eliminado)
                            {
                                await polizaRepository.DeleteAsync(polizaExistente);
                            }
                            else
                            {
                                polizaExistente.NumeroPoliza = poliza.NumeroPoliza;
                                polizaExistente.IdCompaniaAseguradora = poliza.IdCompaniaAseguradora;
                                polizaExistente.IdTipoPoliza = poliza.IdTipoPoliza;
                                polizaExistente.Monto = poliza.Monto;
                                polizaExistente.IdMoneda = poliza.IdMoneda;
                                polizaExistente.FechaInicio = poliza.FechaInicio;
                                polizaExistente.FechaFin = poliza.FechaFin;
                                polizaExistente.NombreArchivo = poliza.NombreArchivo;
                                await polizaRepository.UpdateAsync(polizaExistente);
                            }
                        }
                        else
                        {
                            poliza.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioRegistro);
                            await polizaRepository.AddAsync(poliza);
                        }
                    }

                    foreach (var historial in historialContratoEntidad)
                    {
                        historial.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioModificacion.Value);
                        await historialEventoRepository.AddAsync(historial);
                    }

                    foreach (var adenda in nuevasAdendasContratoEntidad)
                    {
                        var adendaExistente = AdendasActuales.FirstOrDefault(a => a.Id == adenda.Id);
                        if (adendaExistente != null)
                        {
                            if (adenda.Eliminado)
                            {     
                                await adendaRepository.DeleteAsync(adendaExistente);
                            }
                            else
                            {
                                adendaExistente.CodigoAdenda = adenda.CodigoAdenda;
                                adendaExistente.Descripcion = adenda.Descripcion;
                                adendaExistente.IdTipoAdenda = adenda.IdTipoAdenda;
                                adendaExistente.FechaInicio = adenda.FechaInicio;
                                adendaExistente.FechaFin = adenda.FechaFin;
                                adendaExistente.Monto = adenda.Monto;
                                adendaExistente.IdMoneda = adenda.IdMoneda;
                                adendaExistente.NombreArchivo = adenda.NombreArchivo;
                                adendaExistente.IdUsuarioModificacion = adenda.IdUsuarioModificacion;
                                adendaExistente.FechaModificacion = DateTime.Now;
                                await adendaRepository.UpdateAsync(adendaExistente);
                            }
                        }
                        else
                        {
                            adenda.Crear(contratoEntidad.Id, contratoEntidad.IdUsuarioRegistro);
                            await adendaRepository.AddAsync(adenda);
                        }
                    } 

                    await contratoRepository.UpdateAsync(contratoEntidad);
                    await unitOfWork.SaveChangesAsync();
                    await scope.CommitAsync();

                }
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }


        public async Task<bool> derivarContrato(AsigarUsuarioAprobadorDto modelo)
        {
            try
            {
                bool result = false;

                using (var scope = await unitOfWork.BeginTransactionAsync())
                {
                    var contratoEntidad = await contratoRepository.Get(t => t.Id == modelo.Id);

                    if (contratoEntidad == null)
                    {
                        throw new Exception("Contrato no encontrado");
                    }

                    contratoEntidad.IdEstado = modelo.IdEstado;
                    contratoEntidad.IdUsuarioModificacion = modelo.IdUsuarioModificacion;
                    contratoEntidad.FechaModificacion = DateTime.Now;

                    contratoEntidad.IdUsuarioAprobadorContrato = modelo.IdUsuarioAprobadorContrato;

                    await contratoRepository.UpdateAsync(contratoEntidad);

                    HistorialEvento historial = new HistorialEvento();
                    historial.Descripcion = modelo.Evento;
                    historial.Crear(contratoEntidad.Id, modelo.IdUsuarioModificacion);
                    await historialEventoRepository.AddAsync(historial);

                    await unitOfWork.SaveChangesAsync();
                    await scope.CommitAsync();
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        //
        public async Task<bool> accionContrato(AccionContratoDto modelo)
        {
            try
            {
                bool result = false;

                using (var scope = await unitOfWork.BeginTransactionAsync())
                {
                    var contratoEntidad = await contratoRepository.Get(t => t.Id == modelo.Id);

                    if (contratoEntidad == null)
                    {
                        throw new Exception("Contrato no encontrado");
                    }

                    contratoEntidad.IdEstado = modelo.IdEstado;
                    contratoEntidad.IdUsuarioModificacion = modelo.IdUsuarioModificacion;
                    contratoEntidad.FechaModificacion = DateTime.Now;
                    contratoEntidad.FechaCierreContrato = DateTime.Now;
                    contratoEntidad.ComentarioCierreContrato = modelo.Comentarios;

                    await contratoRepository.UpdateAsync(contratoEntidad);


                    HistorialEvento historial = new HistorialEvento();
                    historial.Descripcion = modelo.Evento;
                    historial.Crear(contratoEntidad.Id, modelo.IdUsuarioModificacion);
                    await historialEventoRepository.AddAsync(historial);

                    await unitOfWork.SaveChangesAsync();
                    await scope.CommitAsync();
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<List<NotificacionContratoDto>> ObtenerNotificacionesContratos(FiltroNotificacionDto? filtro)
        {
            try
            {
                // Guid para los estados
                Guid IdEnRegistro = new Guid("FD4668DA-0F09-4810-A13E-9E85B50693EA");
                Guid IdEnAprobacion = new Guid("51A3BEB9-0D7F-4147-9E24-6604E7E682D5");
                Guid IdVigente = new Guid("3155E7A5-ADC1-4D29-BA81-83FD95DD7ED5");
                Guid IdVencido = new Guid("72301DAC-FDF1-473C-9F6A-B6192C7F67D5");
                Guid IdCerrado = new Guid("B621A222-C8D5-4CBC-B842-F887D388E9DC");
                Guid IdAnulado = new Guid("4000E95B-8EE3-441E-9C61-6EA3F6183401");
                Guid IdRechazado = new Guid("3666655A-47EE-478B-B6CB-CC9A557E0CFF");
                Guid IdObservado = new Guid("6BC34E35-A223-4FE7-BA8D-E87F2784A83F");

                var queryContratos = await contratoRepository.QuerySql();
                var query = queryContratos.Where(x => x.Eliminado == false);

                // Aplicar filtro por vista
                if (filtro.Vista == "Mis Notificaciones")
                {
                    query = query.Where(x =>
                        (x.IdUsuarioRegistro == filtro.IdUsuarioRegistro &&
                         (x.IdEstado == IdEnRegistro || x.IdEstado == IdVigente ||
                          x.IdEstado == IdVencido || x.IdEstado == IdCerrado ||
                          x.IdEstado == IdAnulado || x.IdEstado == IdRechazado ||
                          x.IdEstado == IdObservado)) ||

                        (x.IdUsuarioAprobadorContrato == filtro.IdUsuarioRegistro && x.IdEstado == IdEnAprobacion)
                    );
                }
                else if (filtro.Vista == "Administrativa")
                {
                    var queryAdmin = await administradoresContratoRepository.QuerySql();
                    var listaIdContratosAdmin = queryAdmin
                        .Where(x => x.IdUsuario == filtro.IdUsuarioRegistro)
                        .Select(x => x.IdContrato)
                        .ToList();

                    query = query.Where(x => listaIdContratosAdmin.Contains(x.Id));
                }
                else 
                {
                    query = query.Where(x => x.FechaFin.HasValue);
                }

                var contratos = query
                    .OrderByDescending(x => x.FechaRegistro).
                    Select(x => new
                {
                    x.Id,
                    x.TituloContrato,
                    x.FechaFin,
                    x.FechaFinReal,
                    x.FechaCierreContrato,
                    x.FechaRegistro,
                    x.FechaModificacion,
                    x.IdEstado,
                    x.IdUsuarioRegistro,
                    x.IdUsuarioAprobadorContrato
                }).ToList();

                var notificaciones = new List<NotificacionContratoDto>();
                DateTime fechaActual = DateTime.Now;

                foreach (var contrato in contratos)
                {
                    DateTime fechaVencimiento = (contrato.FechaFinReal ?? contrato.FechaFin).GetValueOrDefault();
                    int diasRestantes = (fechaVencimiento - fechaActual).Days;
                    string mensaje = "";
                    string tipo = "";
                    DateTime fechaNotificacion = DateTime.Now;

                    if (filtro.Vista == "Mis Notificaciones")
                    {
                        if (contrato.IdEstado == IdEnRegistro)
                        {
                            mensaje = $"Tu contrato '{contrato.TituloContrato}' ha sido Registrado Exitosamente.";
                            tipo = "ContratoRegistro";
                            fechaNotificacion = contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdEnAprobacion)
                        {
                            mensaje = $"Tienes un contrato pendiente de Aprobación: '{contrato.TituloContrato}'.";
                            tipo = "AprobacionPendiente";
                            fechaNotificacion = contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdVigente)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' está Vigente.";
                            tipo = "ContratoVigente";
                            fechaNotificacion = contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdVencido)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ha Vencido.";
                            tipo = "ContratoVencido";
                            fechaNotificacion = fechaVencimiento;
                        }
                        else if (contrato.IdEstado == IdCerrado)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ha sido Cerrado.";
                            tipo = "ContratoCerrado";
                            fechaNotificacion = contrato.FechaCierreContrato ?? contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdAnulado)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ha sido Anulado.";
                            tipo = "ContratoAnulado";
                            fechaNotificacion = contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdRechazado)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ha sido Rechazado.";
                            tipo = "ContratoRechazado";
                            fechaNotificacion = contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                        else if (contrato.IdEstado == IdObservado)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ha sido Observado.";
                            tipo = "ContratoObservado";
                            fechaNotificacion = contrato.FechaModificacion ?? contrato.FechaRegistro;
                        }
                    }
                    else if (filtro.Vista == "Administrativa")
                    {
                        mensaje = $"Fuiste Asignado como Administrador del contrato '{contrato.TituloContrato}'.";
                        tipo = "Administrador";
                        fechaNotificacion = contrato.FechaRegistro;
                    }
                    else
                    {
                        if (diasRestantes > 0)
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' vence en {diasRestantes} días.";
                            tipo = "ProximoVencimiento";
                            fechaNotificacion = fechaVencimiento;
                        }
                        else
                        {
                            mensaje = $"El contrato '{contrato.TituloContrato}' ya venció hace {Math.Abs(diasRestantes)} días.";
                            tipo = "VencimientoPasado";
                            fechaNotificacion = fechaVencimiento;
                        }
                    }

                    string fechaVencimientoFormateada = fechaVencimiento.ToString("d 'de' MMMM 'del' yyyy");
                    string fechaNotiFormateada = fechaNotificacion.ToString("d 'de' MMMM 'del' yyyy");

                    notificaciones.Add(new NotificacionContratoDto
                    {
                        IdContrato = contrato.Id,
                        TituloContrato = contrato.TituloContrato,
                        FechaVencimiento = contrato.FechaCierreContrato ?? fechaVencimiento,
                        DiasRestantes = contrato.FechaCierreContrato.HasValue ? (fechaActual - contrato.FechaCierreContrato.Value).Days : diasRestantes,
                        Mensaje = mensaje,
                        FechaVencimientoLabel = fechaVencimientoFormateada,
                        Tipo = tipo,
                        FechaNotificacion = fechaNotificacion,
                        FechaNotificacionLabel = fechaNotiFormateada
                    });
                }

                return notificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
