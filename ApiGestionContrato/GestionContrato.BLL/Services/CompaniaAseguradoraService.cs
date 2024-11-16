using GestionContrato.BLL.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;

namespace GestionContrato.BLL.Services
{
    public class CompaniaAseguradoraService: ICompaniaAseguradoraService
    {
        private readonly IGenericRepository<CompaniaAseguradora> companiaAseguradoraRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public CompaniaAseguradoraService(IGenericRepository<CompaniaAseguradora> _companiaAseguradoraRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            companiaAseguradoraRepository = _companiaAseguradoraRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<CompaniaAseguradoraDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryCompaniaAseguradora = await companiaAseguradoraRepository.QuerySql();

                if(!string.IsNullOrEmpty(valorBusqueda))
                {
                    queryCompaniaAseguradora = queryCompaniaAseguradora.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaCompaniaAseguradora = queryCompaniaAseguradora
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<CompaniaAseguradoraDto>>(listaCompaniaAseguradora);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<CompaniaAseguradoraDto> crear(CompaniaAseguradoraDto modelo)
        {
            try
            {
                CompaniaAseguradoraDto result = new CompaniaAseguradoraDto();

                var companiaAseguradoraEntidad = mapper.Map<CompaniaAseguradora>(modelo);

                await companiaAseguradoraRepository.AddAsync(companiaAseguradoraEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(CompaniaAseguradoraDto modelo)
        {
            try
            {
                var companiaAseguradoraEncontrado = await companiaAseguradoraRepository.Get(u => u.Id == modelo.Id);

                if (companiaAseguradoraEncontrado == null)
                {
                    throw new TaskCanceledException("CompaniaAseguradora no encontrada");
                }

                mapper.Map(modelo, companiaAseguradoraEncontrado);

                await companiaAseguradoraRepository.UpdateAsync(companiaAseguradoraEncontrado);
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
                var companiaAseguradoraEncontrado = await companiaAseguradoraRepository.Get(u => u.Id == id);

                if (companiaAseguradoraEncontrado == null)
                {
                    throw new TaskCanceledException("CompaniaAseguradora no encontrada");
                }

                companiaAseguradoraEncontrado.Habilitado = false;

                await companiaAseguradoraRepository.UpdateAsync(companiaAseguradoraEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<CompaniaAseguradoraDto> obtenerCompaniaAseguradoraId(Guid id)
        {
            try
            {
                var queryCompaniaAseguradora = await companiaAseguradoraRepository.QuerySql(u => u.Id == id);
                var companiaAseguradora = queryCompaniaAseguradora
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (companiaAseguradora == null)
                {
                    throw new TaskCanceledException("CompaniaAseguradora no encontrada");
                }

                return mapper.Map<CompaniaAseguradoraDto>(companiaAseguradora);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
