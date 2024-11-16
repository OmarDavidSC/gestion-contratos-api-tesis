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
    public class TipoDocumentoService: ITipoDocumentoService
    {
        private readonly IGenericRepository<TipoDocumento> tipoDocumentoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public TipoDocumentoService(IGenericRepository<TipoDocumento> _tipoDocumentoRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            tipoDocumentoRepository = _tipoDocumentoRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<TipoDocumentoDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querytTipoDocumento = await tipoDocumentoRepository.QuerySql();

                if (!string.IsNullOrEmpty(valorBusqueda))
                {
                    querytTipoDocumento = querytTipoDocumento.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaTipoDocumentos = querytTipoDocumento
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .OrderBy(x => x.Nombre)
                                                   .ToList();

                return mapper.Map<List<TipoDocumentoDto>>(listaTipoDocumentos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoDocumentoDto> crear(TipoDocumentoDto modelo)
        {
            try
            {
                TipoDocumentoDto result = new TipoDocumentoDto();

                var tipoDocumentoEndidad = mapper.Map<TipoDocumento>(modelo);

                await tipoDocumentoRepository.AddAsync(tipoDocumentoEndidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(TipoDocumentoDto modelo)
        {
            try
            {
                var tipoDocumentoEncontrada = await tipoDocumentoRepository.Get(g => g.Id == modelo.Id);

                if (tipoDocumentoEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Documento no Encontrada");
                }

                mapper.Map(modelo, tipoDocumentoEncontrada);

                await tipoDocumentoRepository.UpdateAsync(tipoDocumentoEncontrada);
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
                var tipoDocumentoEncontrada = await tipoDocumentoRepository.Get(g => g.Id == id);

                if (tipoDocumentoEncontrada == null)
                {
                    throw new TaskCanceledException("Tipo Documento no Encontrada");
                }

                tipoDocumentoEncontrada.Habilitado = false;

                await tipoDocumentoRepository.UpdateAsync(tipoDocumentoEncontrada);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<TipoDocumentoDto> obtenerTipoDocumentoId(Guid id)
        {
            try
            {
                var querytTipoDocumento = await tipoDocumentoRepository.QuerySql(g => g.Id == id);
                var tipoDocumento = querytTipoDocumento
                                                   .Include(u => u.UsuarioRegistro)
                                                   .Include(u => u.UsuarioModificacion)
                                                   .FirstOrDefault();

                if (tipoDocumento == null)
                {
                    throw new TaskCanceledException("Tipo Documento no encontrada");
                }

                return mapper.Map<TipoDocumentoDto>(tipoDocumento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
