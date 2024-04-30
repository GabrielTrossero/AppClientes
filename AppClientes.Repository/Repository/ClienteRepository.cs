using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppClientes.Model.Entities;
using AppClientes.Model.Interfaces;
using AppClientes.Repository.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace AppClientes.Repository.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppClientesContext _context;

        public ClienteRepository(AppClientesContext context)
        {
            _context = context;
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = _context.Cliente.ToList();

            return clientes;
        }

        public Cliente? GetId(int idCliente)
        {
            Cliente? cliente = _context.Cliente.Find(idCliente);

            return cliente;
        }

        public bool InsertCliente(Cliente cliente)
        {
            
            if (_context.Cliente.Any(c => c.Id == cliente.Id))
            {
                throw new CustomException($"Error: Duplicación de clave");
            }

            _context.Cliente.Add(cliente);
                 
            _context.SaveChanges();

            return true;
           
        }

        public bool UpdateCliente(Cliente cliente)
        {   
            var clienteExistente = _context.Cliente.Find(cliente.Id);

            if (clienteExistente != null)
            {
                if (!string.IsNullOrEmpty(cliente.Nombres)) clienteExistente.Nombres = cliente.Nombres;
                if (!string.IsNullOrEmpty(cliente.Apellidos)) clienteExistente.Apellidos = cliente.Apellidos;
                if (cliente.FechaNac != null) clienteExistente.FechaNac = cliente.FechaNac;
                if (!string.IsNullOrEmpty(cliente.Cuit)) clienteExistente.Cuit = cliente.Cuit;
                if (!string.IsNullOrEmpty(cliente.Domicilio)) clienteExistente.Domicilio = cliente.Domicilio;
                if (!string.IsNullOrEmpty(cliente.Telefono)) clienteExistente.Telefono = cliente.Telefono;
                if (!string.IsNullOrEmpty(cliente.Email)) clienteExistente.Email = cliente.Email;
                _context.SaveChanges();

                return true;
            }
            else
            {
                throw new CustomException($"Error: Cliente no encontrado");
            }
        }

        public List<Cliente> SearchClientes(string? nombreSearch, string? apellidoSearch)
        {
            var clientes = new List<Cliente>();
            
            if (nombreSearch != null && apellidoSearch != null)
            {
                clientes = _context.Cliente.Where(c => c.Nombres.Contains(nombreSearch) && c.Apellidos.Contains(apellidoSearch)).ToList();
            }
            else if (nombreSearch != null)
            {
                clientes = _context.Cliente.Where(c => c.Nombres.Contains(nombreSearch)).ToList();
            }
            else if (apellidoSearch != null)
            {
                clientes = _context.Cliente.Where(c => c.Apellidos.Contains(apellidoSearch)).ToList();
            }

            return clientes;
        }
    }
}
