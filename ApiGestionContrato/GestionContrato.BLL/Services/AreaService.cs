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
    public class AreaService : IAreaService
    {
        private readonly IGenericRepository<Area> areaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public AreaService(IGenericRepository<Area> _areaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            areaRepository = _areaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<AreaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryArea = await areaRepository.QuerySql();
                if (!string.IsNullOrEmpty(valorBusqueda))
                {
                    queryArea = queryArea.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaArea = queryArea
                                     .Include(a => a.UsuarioResponsable)
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<AreaDto>>(listaArea);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<AreaDto> crear(AreaDto modelo)
        {
            try
            {
                AreaDto result = new AreaDto();

                var areaEntidad = mapper.Map<Area>(modelo);

                await areaRepository.AddAsync(areaEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(AreaDto modelo)
        {
            try
            {
                var areaEncontrado = await areaRepository.Get(u => u.Id == modelo.Id);

                if (areaEncontrado == null)
                {
                    throw new TaskCanceledException("Area no encontrada");
                }

                mapper.Map(modelo, areaEncontrado);

                await areaRepository.UpdateAsync(areaEncontrado);
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
                var areaEncontrado = await areaRepository.Get(u => u.Id == id);

                if (areaEncontrado == null)
                {
                    throw new TaskCanceledException("Area no encontrada");
                }

                areaEncontrado.Habilitado = false;

                await areaRepository.UpdateAsync(areaEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }        

        public async Task<AreaDto> obtenerAreaId(Guid id)
        {
            try
            {
                var queryArea = await areaRepository.QuerySql(u => u.Id == id);
                var area = queryArea
                                     .Include(a => a.UsuarioResponsable)
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (area == null)
                {
                    throw new TaskCanceledException("Area no encontrada");
                }

                return mapper.Map<AreaDto>(area);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
