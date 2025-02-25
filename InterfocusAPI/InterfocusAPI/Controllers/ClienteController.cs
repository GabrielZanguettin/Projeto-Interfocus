using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using InterfocusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Engine.Query;
using NHibernate.Linq.Functions;

namespace InterfocusAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;
        public ClienteController(ClienteService clienteService)
        {
            this.clienteService = clienteService;
        }
        [HttpGet("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await clienteService.MostrarClientes();
                return Ok(clientes);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
            
        }
        [HttpGet("clientes/{nome}")]
        public async Task<IActionResult> ProcurarNome(string nome)
        {
            try
            {
                var cliente = await clienteService.ProcurarPorNome(nome);
                return Ok(cliente);
            }
            catch (Exception error)
            {
                return NotFound(error.Message);
            }
        }
        [HttpPost("clientes")]
        public async Task<IActionResult> Cadastrar([FromBody] Cliente cliente)
        {
            try
            {
                if (await clienteService.ValidarCliente(cliente.CPF, cliente.Email))
                {
                    await clienteService.CadastrarCliente(cliente);
                    return Ok(cliente);
                }
                return BadRequest();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPut("clientes/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDTO clientedto)
        {
            try
            {
                var cliente = await clienteService.AtualizarCliente(id, clientedto);
                return Ok(cliente);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }

        }
        [HttpDelete("clientes/{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var cliente = await clienteService.RemoverCliente(id);
                return Ok(cliente);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
