using GestionContrato.DAL.DBContext;
using GestionContrato.DAL.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.DAL.Repositorios
{
    public class UnitOfWork: IUnitOfWork
    {
        private GestionContratoContext context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(GestionContratoContext _contex)
        {
            context = _contex;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await context.Database.BeginTransactionAsync();
            }
            return _transaction;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            context.Dispose();
            if (_transaction != null)
            {
                _transaction.Dispose();
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
