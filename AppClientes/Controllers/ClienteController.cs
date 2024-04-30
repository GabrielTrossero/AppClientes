using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppClientes.Model.Services;
using AppClientes.Model.Entities;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AppClientes.WebAPI.Models;
using System.Collections.Generic;
using AppClientes.Repository.Exceptions;

namespace AppClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IClienteService clienteService; 
        private readonly ILogger<ClienteController> logger;

        public ClienteController(IClienteService clienteService, IMapper mapper, ILogger<ClienteController> logger)
        {
            this.clienteService = clienteService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Route("getClientes")]
        public IActionResult GetAll()
        {
            try
            {
                List<Cliente> clientes = this.clienteService.GetAll();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error");
            }
        }

        [HttpGet]
        [Route("getCliente")]
        public IActionResult GetId(int idCliente)
        {
            try
            {
                Cliente? cliente = this.clienteService.GetId(idCliente);

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error");
            }
        }

        [HttpPost]
        [Route("insertCliente")]
        public IActionResult InsertCliente([FromBody] Cliente cliente)
        {
            try
            {
                // Validación de los parametros de entrada
                if (!ModelState.IsValid || cliente.Id == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Parametros incorrectos");
                }

                return Ok(this.clienteService.InsertCliente(cliente));
            }
            catch (CustomException ex)
            {
                this.logger.LogInformation(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }   
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error");
            }
        }

        [HttpPut]
        [Route("updateCliente")]
        public IActionResult UpdateCliente([FromBody] ClienteUpdate clienteUpdate)
        {
            try
            {
                // Validación de los parametros de entrada
                if (!ModelState.IsValid || clienteUpdate.Id == 0)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Parametros incorrectos");
                }

                // Mapeo entre ClienteUpdate y Cliente
                Cliente cliente = mapper.Map<Cliente>(clienteUpdate);

                return Ok(this.clienteService.UpdateCliente(cliente));
            }
            catch (CustomException ex)
            {
                this.logger.LogInformation(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error");
            }
        }


        [HttpGet]
        [Route("searchClientes")]
        public IActionResult SearchClientes(string? nombreSearch, string? apellidoSearch)
        {
            try
            {
                List<Cliente> clientes = this.clienteService.SearchClientes(nombreSearch, apellidoSearch);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error");
            }
        }
    }
}
