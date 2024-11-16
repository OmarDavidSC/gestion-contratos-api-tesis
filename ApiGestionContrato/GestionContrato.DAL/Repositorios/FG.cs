using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.DAL.Repositorios
{
    public class FG<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        // Constructor para respuesta exitosa
        public FG(bool success, T data, string message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        // Constructor para un error
        public FG(string message)
        {
            Success = false;
            Data = default(T);
            Message = message;
        }
    }
}
