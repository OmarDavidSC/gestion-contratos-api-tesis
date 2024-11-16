using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.DAL.Repositorios.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //metodos globales
        Task<TModel> Get(Expression<Func<TModel, bool>> expression);
        Task<TModel> CreateAsync(TModel model);
        Task AddAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(TModel model);
        Task<IQueryable<TModel>> QuerySql(Expression<Func<TModel, bool>> expression = null);
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate);
        Task<string> ExecuteProcedure(string procedureName, params SqlParameter[] parameters);
    }
}
