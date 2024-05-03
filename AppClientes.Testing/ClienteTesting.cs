using AppClientes.ApplicationServices.Services;
using AppClientes.Controllers;
using AppClientes.Model.Entities;
using AppClientes.Model.Interfaces;
using AppClientes.Model.Services;
using AppClientes.Repository.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AppClientes.Testing
{
    public class AppClientesContextFake : AppClientesContext
    {
        public AppClientesContextFake(DbContextOptions<AppClientesContext> options)
            : base(options)
        {
        }
    }


    public class ClienteTesting
    {
        private readonly ClienteController clienteController;

        public ClienteTesting()
        {
            // Configurar conexion BD REAL
            var options = new DbContextOptionsBuilder<AppClientesContext>()
                .UseSqlServer("Server=DESKTOP-8FF5L5M\\SQLEXPRESS;Database=AppClientes;Trusted_Connection=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

            // Crear una instancia de tu contexto de base de datos utilizando las opciones configuradas
            var clienteContext = new AppClientesContext(options.Options);

            /*
            // Configurar conexion BD FICTICIA
            var options = new DbContextOptionsBuilder<AppClientesContext>()
                .UseInMemoryDatabase(databaseName: "AppClientes")
                .Options;

            var clienteContext = new AppClientesContextFake(options);
            */

            // Inicializar otras dependencias necesarias para ClienteController (p. ej., IMapper, ILogger)
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<ClienteController>>();

            // Crear instancia de ClienteController utilizando las dependencias configuradas
            var clienteRepository = new ClienteRepository(clienteContext);
            var clienteService = new ClienteService(clienteRepository);
            this.clienteController = new ClienteController(clienteService, mapperMock.Object, loggerMock.Object);
        }

        [Fact]
        public void GetAll_Ok()
        {
            var result = clienteController.GetAll();
            Assert.IsType<OkObjectResult>(result); // Comprueba si devuelve un 200 cuando se ejecuta
        }

        [Fact]
        public void GetAll_Quantity()
        {
            var result = (OkObjectResult)clienteController.GetAll();
            var clientes = Assert.IsType<List<Cliente>>(result.Value); // Comprueba de que sea del tipo List<Cliente>
            Assert.True(clientes.Count > 0); // Comprueba de que haya al menos un cliente
        }

        [Fact]
        public void GetId_Ok()
        {
            var id = 3; // Suponemos que el cliente id=3 siempre existe
            var result = clienteController.GetId(id);
            Assert.IsType<OkObjectResult>(result);  // Comprueba si devuelve un 200 cuando se ejecuta
        }

        [Fact]
        public void GetId_Exists()
        {
            var id = 3; // Suponemos que el cliente id=3 siempre existe
            var result = (OkObjectResult)clienteController.GetId(id);
            var cliente = Assert.IsType<Cliente>(result?.Value); // Comprueba de que sea del tipo Cliente
            Assert.True(cliente != null);
            Assert.Equal(cliente?.Id, id);
        }

        [Fact]
        public void GetId_NoExists()
        {
            var id = 0; // Suponemos que el cliente id=0 nunca existe
            var result = clienteController.GetId(id);
            Assert.IsType<NotFoundResult>(result);  // Comprueba si devuelve un 404 cuando se ejecuta
        }
    }
}