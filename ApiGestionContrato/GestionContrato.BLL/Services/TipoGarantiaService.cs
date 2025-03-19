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
    public class TipoGarantiaService : ITipoGarantiaService
    {
        private readonly IGenericRepository<TipoGarantia> tipoGarantiaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public TipoGarantiaService(IGenericRepository<TipoGarantia> _tipoGarantiaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            tipoGarantiaRepository = _tipoGarantiaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TipoGarantiaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querytTipoGarantia = await tipoGarantiaRepository.QuerySql();

                if (!string.IsNullOrEmpty(valorBusqueda))
                {
                    querytTipoGarantia = querytTipoGarantia.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaTipoGarantias = querytTipoGarantia
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();

                return mapper.Map<List<TipoGarantiaDto>>(listaTipoGarantias);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoGarantiaDto> crear(TipoGarantiaDto modelo)
        {
            try
            {
                TipoGarantiaDto result = new TipoGarantiaDto();

                var tipoGarantiaEndidad = mapper.Map<TipoGarantia>(modelo);

                tipoGarantiaEndidad.FechaRegistro = DateTime.Now;
                tipoGarantiaEndidad.FechaModificacion = DateTime.Now;
                await tipoGarantiaRepository.AddAsync(tipoGarantiaEndidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(TipoGarantiaDto modelo)
        {
            try
            {
                var tipoGarantiaEncontrada = await tipoGarantiaRepository.Get(g => g.Id == modelo.Id);

                if(tipoGarantiaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Garantia no Encontrada");
                }

                mapper.Map(modelo, tipoGarantiaEncontrada);

                tipoGarantiaEncontrada.FechaModificacion = DateTime.Now;
                await tipoGarantiaRepository.UpdateAsync(tipoGarantiaEncontrada);
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
                var tipoGarantiaEncontrada = await tipoGarantiaRepository.Get(g => g.Id == id);

                if (tipoGarantiaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Garantia no Encontrada");
                }

                tipoGarantiaEncontrada.Habilitado = false;

                await tipoGarantiaRepository.UpdateAsync(tipoGarantiaEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }        

        public async Task<TipoGarantiaDto> obtenerTipoGarantiaId(Guid id)
        {
            try
            {
                var querytTipoGarantia = await tipoGarantiaRepository.QuerySql(g => g.Id == id);
                var tipoGarantia = querytTipoGarantia
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .FirstOrDefault();

                if(tipoGarantia == null)
                {
                    throw new TaskCanceledException("Tipo Garantia no encontrada");
                }

                return mapper.Map<TipoGarantiaDto>(tipoGarantia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
