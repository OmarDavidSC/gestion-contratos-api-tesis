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
    public class MetodoEntregaService : IMetodoEntregaService
    {
        private readonly IGenericRepository<MetodoEntrega> metodoEntregaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MetodoEntregaService(IGenericRepository<MetodoEntrega> _metodoEntregaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            metodoEntregaRepository = _metodoEntregaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<MetodoEntregaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryMetodoEntrega = await metodoEntregaRepository.QuerySql();
                if (!string.IsNullOrWhiteSpace(valorBusqueda))
                {
                    queryMetodoEntrega = queryMetodoEntrega.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaMetodoEntrega = queryMetodoEntrega
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<MetodoEntregaDto>>(listaMetodoEntrega);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<MetodoEntregaDto> crear(MetodoEntregaDto modelo)
        {
            try
            {
                MetodoEntregaDto result = new MetodoEntregaDto();

                var metodoEntregaEntidad = mapper.Map<MetodoEntrega>(modelo);

                await metodoEntregaRepository.AddAsync(metodoEntregaEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(MetodoEntregaDto modelo)
        {
            try
            {
                var metodoEntregaEncontrado = await metodoEntregaRepository.Get(u => u.Id == modelo.Id);

                if (metodoEntregaEncontrado == null)
                {
                    throw new TaskCanceledException("Metodo de Entrega no encontrada");
                }

                mapper.Map(modelo, metodoEntregaEncontrado);

                await metodoEntregaRepository.UpdateAsync(metodoEntregaEncontrado);
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
                var metodoEntregaEncontrado = await metodoEntregaRepository.Get(u => u.Id == id);

                if (metodoEntregaEncontrado == null)
                {
                    throw new TaskCanceledException("Metodo de entrega no encontrada");
                }

                metodoEntregaEncontrado.Habilitado = false;

                await metodoEntregaRepository.UpdateAsync(metodoEntregaEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }        

        public async Task<MetodoEntregaDto> obtenerMetodoEntregaId(Guid id)
        {
            try
            {
                var queryMetodoEntrega = await metodoEntregaRepository.QuerySql(u => u.Id == id);
                var metodoEntrega = queryMetodoEntrega
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (metodoEntrega == null)
                {
                    throw new TaskCanceledException("Metodo de Entrega no encontrada");
                }

                return mapper.Map<MetodoEntregaDto>(metodoEntrega);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
