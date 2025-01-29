using AutoMapper;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IGenericRepository<Usuario> usuarioRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UsuarioService
        (
        IGenericRepository<Usuario> _usuarioRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
        )
        {
            usuarioRepository = _usuarioRepository;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<List<UsuarioDto>> lista(string? valorBusqueda)
        {
            try
            {
                var queryUsuario = await usuarioRepository.QuerySql();

                if (!string.IsNullOrWhiteSpace(valorBusqueda))
                {
                    queryUsuario = queryUsuario.Where(x => x.Nombre.Contains(valorBusqueda));
                }

                var listarUsuario = queryUsuario
                                          .Include(u => u.Area)
                                          .Include(u => u.UsuarioRegistro)
                                          .Include(u => u.UsuarioModificacion)
                                          .OrderBy(x => x.Nombre)
                                          .ToList();

                return mapper.Map<List<UsuarioDto>>(listarUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<UsuarioDto> crear(UsuarioDto modelo)
        {
            try
            {
                UsuarioDto result = new UsuarioDto();

                //encriptacion
                if (!string.IsNullOrEmpty(modelo.Clave))
                {
                    modelo.Clave = BCrypt.Net.BCrypt.HashPassword(modelo.Clave);
                }

                var usuarioEntidad = mapper.Map<Usuario>(modelo);

                await usuarioRepository.AddAsync(usuarioEntidad);
                await unitOfWork.SaveChangesAsync();

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> editar(UsuarioDto modelo)
        {
            try
            {
                var usuarioEncontrado = await usuarioRepository.Get(u => u.Id == modelo.Id);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }

                string claveActual = usuarioEncontrado.Clave;

                mapper.Map(modelo, usuarioEncontrado);

                if (string.IsNullOrEmpty(modelo.Clave))
                {
                    usuarioEncontrado.Clave = claveActual;
                }


                await usuarioRepository.UpdateAsync(usuarioEncontrado);
                await unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> eliminar(Guid id)
        {
            try
            {
                var usuarioEncontado = await usuarioRepository.Get(u => u.Id == id);

                if (usuarioEncontado == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }

                usuarioEncontado.Habilitado = false;

                await usuarioRepository.UpdateAsync(usuarioEncontado);
                await unitOfWork.SaveChangesAsync();

                return true;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDto> obtenerUsuarioId(Guid id)
        {
            try
            {
                var queryUsuario = await usuarioRepository.QuerySql(u => u.Id == id);
                var usuario = queryUsuario.Include(u => u.Area)
                                                     .Include(u => u.UsuarioRegistro)
                                                     .Include(u => u.UsuarioModificacion)
                                                     .FirstOrDefault();


                if (usuario == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }

                return mapper.Map<UsuarioDto>(usuario);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<UsuarioDto>> buscarUsuarios(string palabra)
        {
            try
            {
                var queryUsuarios = await usuarioRepository.QuerySql(
                    u => u.NombreCompleto.Contains(palabra) || u.Correo.Contains(palabra));

                var listarUsuarios = queryUsuarios
                                        .Include(u => u.Area)
                                        .ToList();

                return mapper.Map<List<UsuarioDto>>(listarUsuarios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioDto> validarUsuarioEmail(string email)
        {
            try
            {
                var usuario = await usuarioRepository.Get(u => u.Correo == email);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                return mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> restablcerContrseana(string email, string nuevaContrasena)
        {
            try
            {
                var usuario = await usuarioRepository.Get(u => u.Correo == email);
                if(usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
                await usuarioRepository.UpdateAsync(usuario);
                await unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
