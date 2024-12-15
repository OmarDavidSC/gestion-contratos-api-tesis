using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class DashBoardService : IDashboardService
    {
        public readonly IGenericRepository<Contrato> contratoRepostory;

        public DashBoardService(IGenericRepository<Contrato> _contratoRepostory)
        {
            contratoRepostory = _contratoRepostory;
        }

        public async Task<List<DashEstadoDto>> contratosPorEstado()
        {
            try
            {
                var queryContrato = await contratoRepostory.QuerySql();

                var estados = new Dictionary<Guid, string>
                {
                    { new Guid("FD4668DA-0F09-4810-A13E-9E85B50693EA"), "En Registro" },
                    { new Guid("51A3BEB9-0D7F-4147-9E24-6604E7E682D5"), "En Aprobación" },
                    { new Guid("3155E7A5-ADC1-4D29-BA81-83FD95DD7ED5"), "Vigente" },
                    { new Guid("72301DAC-FDF1-473C-9F6A-B6192C7F67D5"), "Vencido" },
                    { new Guid("B621A222-C8D5-4CBC-B842-F887D388E9DC"), "Cerrado" },
                    { new Guid("4000E95B-8EE3-441E-9C61-6EA3F6183401"), "Anulado" },
                    { new Guid("3666655A-47EE-478B-B6CB-CC9A557E0CFF"), "Rechazado" },
                    { new Guid("6BC34E35-A223-4FE7-BA8D-E87F2784A83F"), "Observado" }
                };

                var resultado = queryContrato
                     .GroupBy(c => c.IdEstado)
                     .Select(g => new DashEstadoDto
                     {
                         Estado = g.Key.HasValue && estados.ContainsKey(g.Key.Value)
                             ? estados[g.Key.Value]
                             : "Estado Desconocido",
                         Cantidad = g.Count()
                     })
                     .ToList();

                resultado.Add(new DashEstadoDto
                {
                    Estado = "Total General",
                    Cantidad = resultado.Sum(r => r.Cantidad)
                });

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DashPorProveedorDto>> contratosPorProveedor()
        {
            try
            {
                var queryContrato = await contratoRepostory.QuerySql();

                if (queryContrato == null || !queryContrato.Any())
                {
                    return new List<DashPorProveedorDto>();
                }

                var resultado = queryContrato
                    .Where(c => c.Proveedor != null && !string.IsNullOrEmpty(c.Proveedor.Nombre))
                    .GroupBy(c => c.Proveedor!.Nombre)
                    .Select(g => new DashPorProveedorDto
                    {
                        NombreProveedor = g.Key, 
                        cantidadContratos = g.Count()
                    })
                    .ToList();

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DashTipoContratoDto>> contratosPorTipo()
        {
            try
            {
                var queryContrato = await contratoRepostory.QuerySql();
                if (queryContrato == null || !queryContrato.Any())
                {
                    return new List<DashTipoContratoDto>();
                }

                var resultado = queryContrato
                    .Where(c => c.TipoContrato != null && !string.IsNullOrEmpty(c.TipoContrato.Nombre))
                    .GroupBy(c => c.TipoContrato!.Nombre)
                    .Select(g => new DashTipoContratoDto
                    {
                        TipoContraro = g.Key, 
                        cantidad = g.Count()
                    })
                    .ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<List<DashPorMesDto>> contratosPorMes()
        {
            try
            {
                var queryContrato = await contratoRepostory.QuerySql();

                if (queryContrato == null || !queryContrato.Any())
                {
                    return new List<DashPorMesDto>();
                }

                // Agrupar contratos por mes
                var resultado = queryContrato
                    //.Where(c => c.FechaRegistro.HasValue)
                    .GroupBy(c => new
                    {
                        Año = c.FechaRegistro.Year,
                        Mes = c.FechaRegistro.Month
                    })
                    .Select(g => new DashPorMesDto
                    {
                        Anio = g.Key.Año,
                        Mes = g.Key.Mes,
                        CantidadContratos = g.Count()
                    })
                    .OrderBy(r => r.Anio)
                    .ThenBy(r => r.Mes)
                    .ToList();

                return resultado;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
