using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using InterfocusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq.Functions;

namespace InterfocusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;
        public ClienteController(ClienteService clienteService)
        {
            this.clienteService = clienteService;
        }
        [HttpGet("verclientes")]
        public IActionResult GetClientes()
        {
            var clientes = clienteService.MostrarClientes();
            return Ok(clientes);
        }
        [HttpGet("procurarnome")]
        public IActionResult ProcurarNome(string nome)
        {
            var cliente = clienteService.ProcurarPorNome(nome);
            return Ok(cliente);
        }
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            if (clienteService.ValidarCliente(cliente.CPF, cliente.Email))
            {
                clienteService.CadastrarCliente(cliente);
                return Ok(cliente);
            }
            return BadRequest();
        }
        [HttpPut("atualizarcliente")]
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clientedto)
        {
            var cliente = clienteService.AtualizarCliente(id, clientedto);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        [HttpDelete("deletarcliente")]
        public IActionResult Deletar(int id)
        {
            var cliente = clienteService.RemoverCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
    }
}
