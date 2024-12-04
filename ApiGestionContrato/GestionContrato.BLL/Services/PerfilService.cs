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
    public class PerfilService : IPerfilService
    {
        private readonly IGenericRepository<Usuario> usuarioRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PerfilService
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

        public async Task<PerfilDto> show(Guid id)
        {
            try
            {
                var usuario = await usuarioRepository.Get(u => u.Id == id);

                if(usuario ==  null)
                {
                    throw new TaskCanceledException("Perfil no encontrado");
                }
                 var perfil = mapper.Map<PerfilDto>(usuario);

                return perfil;

            }
            catch(Exception ex)
            {
                throw new Exception($"Error al obtener el pefil : {ex.Message}");
            }
        }

        public async Task<PerfilDto> update(PerfilDto modelo)
        {
            try
            {
                var usuarioEncontrado = await usuarioRepository.Get(u => u.Id == modelo.Id);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Perfil no encontrado");
                }

                // Actualizar los campos de usuario
                usuarioEncontrado.Nombre = modelo.Nombre;
                usuarioEncontrado.ApellidoPaterno = modelo.ApellidoPaterno;
                usuarioEncontrado.ApellidoMaterno = modelo.ApellidoMaterno;
                usuarioEncontrado.Correo = modelo.Correo;

                usuarioEncontrado.FechaModificacion = DateTime.Now;

                await usuarioRepository.UpdateAsync(usuarioEncontrado);
                await unitOfWork.SaveChangesAsync();

                var perfilActualizado = mapper.Map<PerfilDto>(usuarioEncontrado);
                return perfilActualizado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el perfil: {ex.Message}");
            }
        }
    }
}
