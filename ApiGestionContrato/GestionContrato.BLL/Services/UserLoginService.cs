using AutoMapper;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IGenericRepository<Usuario> usuarioRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public UserLoginService
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

        public async Task<UsuarioDto> iniciarSesion(string Correo, string Clave)
        {
            try
            {
                var queryUsuario = await usuarioRepository.QuerySql(u => u.Correo == Correo
                                                                     && u.Clave == Clave);
                var usuario = queryUsuario.FirstOrDefault();
                if (usuario == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }     
                
                if(!usuario.Habilitado)
                {
                    throw new TaskCanceledException("El usuario esta deshabilitado. Comuniquese con el Administrador");
                }

                return new UsuarioDto
                {
                    Id = usuario.Id,
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
