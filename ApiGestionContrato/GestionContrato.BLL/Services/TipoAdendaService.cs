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
    public class TipoAdendaService: ITipoAdendaService
    {
        private readonly IGenericRepository<TipoAdenda> tipoAdendaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public TipoAdendaService(IGenericRepository<TipoAdenda> _tipoAdendaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            tipoAdendaRepository = _tipoAdendaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TipoAdendaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querytTipoAdenda = await tipoAdendaRepository.QuerySql();

                if(!string.IsNullOrEmpty(valorBusqueda))
                {
                    querytTipoAdenda = querytTipoAdenda.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaTipoAdendas = querytTipoAdenda
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();

                return mapper.Map<List<TipoAdendaDto>>(listaTipoAdendas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoAdendaDto> crear(TipoAdendaDto modelo)
        {
            try
            {
                TipoAdendaDto result = new TipoAdendaDto();

                var tipoAdendaEndidad = mapper.Map<TipoAdenda>(modelo);

                await tipoAdendaRepository.AddAsync(tipoAdendaEndidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(TipoAdendaDto modelo)
        {
            try
            {
                var tipoAdendaEncontrada = await tipoAdendaRepository.Get(g => g.Id == modelo.Id);

                if (tipoAdendaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Adenda no Encontrada");
                }

                mapper.Map(modelo, tipoAdendaEncontrada);

                await tipoAdendaRepository.UpdateAsync(tipoAdendaEncontrada);
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
                var tipoAdendaEncontrada = await tipoAdendaRepository.Get(g => g.Id == id);

                if (tipoAdendaEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Adenda no Encontrada");
                }

                tipoAdendaEncontrada.Habilitado = false;

                await tipoAdendaRepository.UpdateAsync(tipoAdendaEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoAdendaDto> obtenerTipoAdendaId(Guid id)
        {
            try
            {
                var querytTipoAdenda = await tipoAdendaRepository.QuerySql(g => g.Id == id);
                var tipoAdenda = querytTipoAdenda
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .FirstOrDefault();

                if (tipoAdenda == null)
                {
                    throw new TaskCanceledException("Tipo Adenda no encontrada");
                }

                return mapper.Map<TipoAdendaDto>(tipoAdenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
