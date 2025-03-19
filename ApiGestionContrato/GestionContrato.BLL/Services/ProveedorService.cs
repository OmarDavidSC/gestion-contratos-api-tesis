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
    public class ProveedorService : IProveedorService
    {
        private readonly IGenericRepository<Proveedor> proveedorRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public ProveedorService(IGenericRepository<Proveedor> _proveedorRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            proveedorRepository = _proveedorRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<ProveedorDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryProveedor = await proveedorRepository.QuerySql();

                if (!string.IsNullOrWhiteSpace(valorBusqueda))
                {
                    queryProveedor = queryProveedor.Where(x => x.Nombre.Contains(valorBusqueda) || x.Ruc.Contains(valorBusqueda));
                }

                var listaProveedor = queryProveedor
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<ProveedorDto>>(listaProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ProveedorDto> crear(ProveedorDto modelo)
        {
            try
            {
                ProveedorDto result = new ProveedorDto();

                var provedorEntidad = mapper.Map<Proveedor>(modelo);

                provedorEntidad.FechaRegistro = DateTime.Now;
                provedorEntidad.FechaModificacion = DateTime.Now;
                await proveedorRepository.AddAsync(provedorEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(ProveedorDto modelo)
        {
            try
            {
                var provedorEncontrado = await proveedorRepository.Get(u => u.Id == modelo.Id);

                if (provedorEncontrado == null)
                {
                    throw new TaskCanceledException("Proveedor no encontrada");
                }

                mapper.Map(modelo, provedorEncontrado);

                provedorEncontrado.FechaModificacion = DateTime.Now;
                await proveedorRepository.UpdateAsync(provedorEncontrado);
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
                var provedorEncontrado = await proveedorRepository.Get(u => u.Id == id);

                if (provedorEncontrado == null)
                {
                    throw new TaskCanceledException("Proveedor no encontrada");
                }

                provedorEncontrado.Habilitado = false;

                await proveedorRepository.UpdateAsync(provedorEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ProveedorDto> obtenerProveedorId(Guid id)
        {
            try
            {
                var queryProveedor = await proveedorRepository.QuerySql(u => u.Id == id);
                var proveedor = queryProveedor
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (proveedor == null)
                {
                    throw new TaskCanceledException("Proveedor no encontrada");
                }

                return mapper.Map<ProveedorDto>(proveedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
