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
    public class MonedaService : IMonedaService
    {
        private readonly IGenericRepository<Moneda> monedaRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MonedaService(IGenericRepository<Moneda> _monedaRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            monedaRepository = _monedaRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<MonedaDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryMoneda = await monedaRepository.QuerySql();

                if(!string.IsNullOrEmpty(valorBusqueda))
                {
                    queryMoneda = queryMoneda.Where(X => X.Nombre.Contains(valorBusqueda));
                }

                var listaMoneda = queryMoneda
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<MonedaDto>>(listaMoneda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<MonedaDto> crear(MonedaDto modelo)
        {
            try
            {
                MonedaDto result = new MonedaDto();

                var monedaEntidad = mapper.Map<Moneda>(modelo);

                monedaEntidad.FechaRegistro = DateTime.Now;
                monedaEntidad.FechaModificacion = DateTime.Now;
                await monedaRepository.AddAsync(monedaEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(MonedaDto modelo)
        {
            try
            {
                var monedaEncontrado = await monedaRepository.Get(u => u.Id == modelo.Id);

                if (monedaEncontrado == null)
                {
                    throw new TaskCanceledException("Moneda no encontrada");
                }

                mapper.Map(modelo, monedaEncontrado);

                monedaEncontrado.FechaModificacion = DateTime.Now;
                await monedaRepository.UpdateAsync(monedaEncontrado);
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
                var monedaEncontrado = await monedaRepository.Get(u => u.Id == id);

                if (monedaEncontrado == null)
                {
                    throw new TaskCanceledException("Moneda no encontrada");
                }

                monedaEncontrado.Habilitado = false;

                await monedaRepository.UpdateAsync(monedaEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }        

        public async Task<MonedaDto> obtenerMonedaId(Guid id)
        {
            try
            {
                var queryMoneda = await monedaRepository.QuerySql(u => u.Id == id);
                var moneda = queryMoneda
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (moneda == null)
                {
                    throw new TaskCanceledException("Moneda no encontrada");
                }

                return mapper.Map<MonedaDto>(moneda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
