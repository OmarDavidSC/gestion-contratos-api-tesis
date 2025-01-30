using AutoMapper;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.Dto;
using GestionContrato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class ResetPasswordService : IResetPassword
    {
        private readonly IGenericRepository<Usuario> usuarioRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ResetPasswordService
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

        public async Task<ResetPasswordDto> update(ResetPasswordDto modelo)
        {
            try
            {
                if(
                    string.IsNullOrWhiteSpace(modelo.NuevaContrasena) ||
                    string.IsNullOrWhiteSpace(modelo.ConfirmarContrasena)
                    )
                {
                    throw new ArgumentException("Campos Obligatorios");
                }

                if (modelo.NuevaContrasena != modelo.ConfirmarContrasena)
                {
                    throw new ArgumentException("Las contraseñas deben de coincidir.");
                }

                var usuarioEncontrado = await usuarioRepository.Get(u => u.Id == modelo.Id);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Perfil no encontrado");
                }

                usuarioEncontrado.Clave = BCrypt.Net.BCrypt.HashPassword(modelo.NuevaContrasena);

                await usuarioRepository.UpdateAsync(usuarioEncontrado);
                await unitOfWork.SaveChangesAsync();

                return new ResetPasswordDto
                {   
                    Id = usuarioEncontrado.Id,
                    NuevaContrasena = usuarioEncontrado.Clave,
                    ConfirmarContrasena = usuarioEncontrado.Clave
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
