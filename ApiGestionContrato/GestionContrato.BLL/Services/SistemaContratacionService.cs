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
    public class SistemaContratacionService : ISistemaContratacionService
    {
        private readonly IGenericRepository<SistemaContratacion> sistemaContratacionRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public SistemaContratacionService(IGenericRepository<SistemaContratacion> _sistemaContratacionRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            sistemaContratacionRepository = _sistemaContratacionRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<SistemaContratacionDto>> listar(string? valorBusqueda)
        {
            try
            {
                var querySistemaContratacion = await sistemaContratacionRepository.QuerySql();

                if(!string.IsNullOrEmpty(valorBusqueda))
                {
                    querySistemaContratacion = querySistemaContratacion.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaSistemaContratacion = querySistemaContratacion
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<SistemaContratacionDto>>(listaSistemaContratacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }            
        }

        public async Task<SistemaContratacionDto> crear(SistemaContratacionDto modelo)
        {
            try
            {
                SistemaContratacionDto result = new SistemaContratacionDto();

                var sistemaContratacionEntidad = mapper.Map<SistemaContratacion>(modelo);

                sistemaContratacionEntidad.FechaRegistro = DateTime.Now;
                sistemaContratacionEntidad.FechaModificacion = DateTime.Now;
                await sistemaContratacionRepository.AddAsync(sistemaContratacionEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }            
        }

        public async Task<bool> editar(SistemaContratacionDto modelo)
        {
            try
            {
                var sistemaContratacionEncontrado = await sistemaContratacionRepository.Get(u => u.Id == modelo.Id);

                if (sistemaContratacionEncontrado == null)
                {
                    throw new TaskCanceledException("SistemaContratacion no encontrada");
                }

                mapper.Map(modelo, sistemaContratacionEncontrado);

                sistemaContratacionEncontrado.FechaModificacion = DateTime.Now;
                await sistemaContratacionRepository.UpdateAsync(sistemaContratacionEncontrado);
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
                var sistemaContratacionEncontrado = await sistemaContratacionRepository.Get(u => u.Id == id);

                if (sistemaContratacionEncontrado == null)
                {
                    throw new TaskCanceledException("Sistema Contratacion no encontrada");
                }

                sistemaContratacionEncontrado.Habilitado = false;

                await sistemaContratacionRepository.UpdateAsync(sistemaContratacionEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }           
        }        

        public async Task<SistemaContratacionDto> obtenerSistemaContratacionId(Guid id)
        {
            try
            {
                var querySistemaContratacion = await sistemaContratacionRepository.QuerySql(u => u.Id == id);
                var sistemaContratacion = querySistemaContratacion
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (sistemaContratacion == null)
                {
                    throw new TaskCanceledException("Sistema Contratacion no encontrada");
                }

                return mapper.Map<SistemaContratacionDto>(sistemaContratacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
