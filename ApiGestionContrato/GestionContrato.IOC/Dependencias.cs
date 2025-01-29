using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.DBContext;
using GestionContrato.DAL.Repositorios.Interfaces;
using GestionContrato.DAL.Repositorios;
using GestionContrato.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.IOC
{
    public static class Dependencias
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GestionContratoContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("cadena"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperModel));

            //Servicios
           
            services.AddScoped<IAreaService,  AreaService>();
            services.AddScoped<IBancoService, BancoService>();
            services.AddScoped<IDashboardService, DashBoardService>();
            services.AddScoped<ICompaniaAseguradoraService, CompaniaAseguradoraService>();
            services.AddScoped<IMetodoEntregaService, MetodoEntregaService>();
            services.AddScoped<IMonedaService, MonedaService>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<ISistemaContratacionService, SistemaContratacionService>();
            services.AddScoped<ITipoGarantiaService,  TipoGarantiaService>();
            services.AddScoped<ITipoContratoService, TipoContratoService>();
            services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
            services.AddScoped<ITipoPolizaService, TipoPolizaService>();
            services.AddScoped<ITipoAdendaService, TipoAdendaService>();            
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IResetPassword, ResetPasswordService>();

            services.AddScoped<IContratoService, ContratoService>();
        }
    }
}
