using AutoMapper;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class TipoPolizaService : ITipoPolizaService
    {
        private readonly IGenericRepository<TipoPoliza> tipoPolizaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public TipoPolizaService(IGenericRepository<TipoPoliza> _tipoPolizaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            tipoPolizaRepository = _tipoPolizaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TipoPolizaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querytTipoPoliza = await tipoPolizaRepository.QuerySql();

                if (!string.IsNullOrEmpty(valorBusqueda))
                {
                    querytTipoPoliza = querytTipoPoliza.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaTipoPolizas = querytTipoPoliza
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();

                return mapper.Map<List<TipoPolizaDto>>(listaTipoPolizas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoPolizaDto> crear(TipoPolizaDto modelo)
        {
            try
            {
                TipoPolizaDto result = new TipoPolizaDto();

                var tipoPolizaEndidad = mapper.Map<TipoPoliza>(modelo);

                tipoPolizaEndidad.FechaRegistro = DateTime.Now;
                tipoPolizaEndidad.FechaModificacion = DateTime.Now;
                await tipoPolizaRepository.AddAsync(tipoPolizaEndidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(TipoPolizaDto modelo)
        {
            try
            {
                var tipoPolizaEncontrada = await tipoPolizaRepository.Get(g => g.Id == modelo.Id);

                if (tipoPolizaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Poliza no Encontrada");
                }

                mapper.Map(modelo, tipoPolizaEncontrada);

                tipoPolizaEncontrada.FechaModificacion = DateTime.Now;
                await tipoPolizaRepository.UpdateAsync(tipoPolizaEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> eliminar(Guid id)
        {
            try
            {
                var tipoPolizaEncontrada = await tipoPolizaRepository.Get(g => g.Id == id);

                if (tipoPolizaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Poliza no Encontrada");
                }

                tipoPolizaEncontrada.Habilitado = false;

                await tipoPolizaRepository.UpdateAsync(tipoPolizaEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoPolizaDto> obtenerTipoPolizaId(Guid id)
        {
            try
            {
                var querytTipoPoliza = await tipoPolizaRepository.QuerySql(g => g.Id == id);
                var tipoPoliza = querytTipoPoliza
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .FirstOrDefault();

                if (tipoPoliza == null)
                {
                    throw new TaskCanceledException("Tipo Poliza no encontrada");
                }

                return mapper.Map<TipoPolizaDto>(tipoPoliza);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
