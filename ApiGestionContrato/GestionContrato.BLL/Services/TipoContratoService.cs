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
    public class TipoContratoService: ITipoContratoService
    {
        private readonly IGenericRepository<TipoContrato> tipoContratoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public TipoContratoService(IGenericRepository<TipoContrato> _tipoContratoRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            tipoContratoRepository = _tipoContratoRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TipoContratoDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querytTipoContrato = await tipoContratoRepository.QuerySql();

                if(!string.IsNullOrEmpty(valorBusqueda))
                {
                    querytTipoContrato = querytTipoContrato.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaTipoContratos = querytTipoContrato
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();

                return mapper.Map<List<TipoContratoDto>>(listaTipoContratos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoContratoDto> crear(TipoContratoDto modelo)
        {
            try
            {
                TipoContratoDto result = new TipoContratoDto();

                var tipoContratoEndidad = mapper.Map<TipoContrato>(modelo);

                tipoContratoEndidad.FechaRegistro = DateTime.Now;
                tipoContratoEndidad.FechaModificacion = DateTime.Now;
                await tipoContratoRepository.AddAsync(tipoContratoEndidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(TipoContratoDto modelo)
        {
            try
            {
                var tipoContratoEncontrada = await tipoContratoRepository.Get(g => g.Id == modelo.Id);

                if (tipoContratoEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Contrato no Encontrado");
                }

                mapper.Map(modelo, tipoContratoEncontrada);

                tipoContratoEncontrada.FechaModificacion = DateTime.Now;
                await tipoContratoRepository.UpdateAsync(tipoContratoEncontrada);
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
                var tipoContratoEncontrada = await tipoContratoRepository.Get(c => c.Id == id);

                if (tipoContratoEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Contrato no Encontrado");
                }

                tipoContratoEncontrada.Habilitado = false;

                await tipoContratoRepository.UpdateAsync(tipoContratoEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoContratoDto> obtenerTipoContratoId(Guid id)
        {
            try
            {
                var querytTipoContrato = await tipoContratoRepository.QuerySql(c => c.Id == id);
                var tipoContrato = querytTipoContrato
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .FirstOrDefault();

                if (tipoContrato == null)
                {
                    throw new TaskCanceledException("Tipo Contrato no encontrado");
                }

                return mapper.Map<TipoContratoDto>(tipoContrato);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
