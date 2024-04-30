using AppClientes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClientes.Model.Services
{
    public interface IClienteService
    {
        List<Cliente> GetAll();
        Cliente? GetId(int idCliente);
        bool InsertCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        List<Cliente> SearchClientes(string? nombreSearch, string? apellidoSearch);
    }
}
