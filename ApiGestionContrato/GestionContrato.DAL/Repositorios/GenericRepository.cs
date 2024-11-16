using GestionContrato.DAL.DBContext;
using GestionContrato.DAL.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.DAL.Repositorios
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly GestionContratoContext context;
        readonly DbSet<TModel> EntitySet;

        public GenericRepository(GestionContratoContext _context)
        {
            context = _context;
            EntitySet = _context.Set<TModel>();
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> expression)
        {
            try
            {
                TModel model = await context.Set<TModel>().FirstOrDefaultAsync(expression);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await EntitySet.Where(predicate).ToListAsync();
        }

        public async Task<TModel> CreateAsync(TModel model)
        {
            try
            {
                context.Set<TModel>().Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task AddAsync(TModel model)
        {
            try
            {
                context.Set<TModel>().Add(model);
                // await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(TModel model)
        {
            try
            {
                context.Set<TModel>().Update(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TModel model)
        {
            try
            {
                context.Set<TModel>().Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> QuerySql(Expression<Func<TModel, bool>> expression = null)
        {
            try
            {
                IQueryable<TModel> queryModel = expression == null ? context.Set<TModel>() : context.Set<TModel>().Where(expression);
                return queryModel;
            }
            catch
            {
                throw;
            }

        }

        public async Task<string> ExecuteProcedure(string procedureName, params SqlParameter[] parameters)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append("EXEC ");
                commandText.Append(procedureName);

                bool firstParam = true;
                foreach (var parameter in parameters)
                {
                    if (firstParam)
                    {
                        commandText.Append(" ");
                        firstParam = false;
                    }
                    else
                    {
                        commandText.Append(", ");
                    }
                    commandText.Append(parameter.ParameterName);

                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        commandText.Append(" OUTPUT");
                    }
                }

                // Ejecutar el comando SQL
                await context.Database.ExecuteSqlRawAsync(commandText.ToString(), parameters);

                // Obtener el valor del parámetro de salida si existe
                var outputParam = parameters.FirstOrDefault(p => p.Direction == ParameterDirection.Output);
                string result = outputParam?.Value?.ToString() ?? string.Empty;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado: " + ex.Message);
            }
        }
    }
}
