using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppClientes.Model.Entities;
using AppClientes.Model.Interfaces;
using AppClientes.Model.Services;

namespace AppClientes.ApplicationServices.Services
{
    public class ClienteService : IClienteService
    {
        /*
         * ACLARACIÓN: este nivel de aplicación por el momento no era necesario
         * ya que los datos no sufren modificación, pero se aplicó a modo de uso
         * para el futuro
         */
        
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public List<Cliente> GetAll()
        {
            return this.clienteRepository.GetAll();
        }

        public Cliente? GetId(int idCliente)
        {
            return this.clienteRepository.GetId(idCliente);
        }

        public bool InsertCliente(Cliente cliente)
        {
            return this.clienteRepository.InsertCliente(cliente);
        }
        
        public bool UpdateCliente(Cliente cliente)
        {
            return this.clienteRepository.UpdateCliente(cliente);
        }

        public List<Cliente> SearchClientes(string? nombreSearch, string? apellidoSearch)
        {
            return this.clienteRepository.SearchClientes(nombreSearch, apellidoSearch);
        }
    }
}
