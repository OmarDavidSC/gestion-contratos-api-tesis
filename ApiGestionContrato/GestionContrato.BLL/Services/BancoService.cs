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
    public class BancoService : IBancoService
    {
        private readonly IGenericRepository<Banco> bancoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public BancoService(IGenericRepository<Banco> _bancoRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            bancoRepository = _bancoRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<BancoDto>> listar(string? valorBusqueda)
        {
            try
            {
                var queryBanco = await bancoRepository.QuerySql();

                if (!string.IsNullOrEmpty(valorBusqueda))
                {
                    queryBanco = queryBanco.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listaBanco = queryBanco
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .OrderBy(x => x.Nombre)
                                     .ToList();

                return mapper.Map<List<BancoDto>>(listaBanco);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BancoDto> crear(BancoDto modelo)
        {
            try
            {
                BancoDto result = new BancoDto();

                var bancoEntidad = mapper.Map<Banco>(modelo);

                await bancoRepository.AddAsync(bancoEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> editar(BancoDto modelo)
        {
            try
            {
                var bancoEncontrado = await bancoRepository.Get(u => u.Id == modelo.Id);

                if (bancoEncontrado == null)
                {
                    throw new TaskCanceledException("Banco no encontrada");
                }

                mapper.Map(modelo, bancoEncontrado);

                await bancoRepository.UpdateAsync(bancoEncontrado);
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
                var bancoEncontrado = await bancoRepository.Get(u => u.Id == id);

                if (bancoEncontrado == null)
                {
                    throw new TaskCanceledException("Banco no encontrada");
                }

                bancoEncontrado.Habilitado = false;

                await bancoRepository.UpdateAsync(bancoEncontrado);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BancoDto> obtenerBancoId(Guid id)
        {
            try
            {
                var queryBanco = await bancoRepository.QuerySql(u => u.Id == id);
                var banco = queryBanco
                                     .Include(a => a.UsuarioRegistro)
                                     .Include(a => a.UsuarioModificacion)
                                     .FirstOrDefault();


                if (banco == null)
                {
                    throw new TaskCanceledException("Banco no encontrada");
                }

                return mapper.Map<BancoDto>(banco);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
