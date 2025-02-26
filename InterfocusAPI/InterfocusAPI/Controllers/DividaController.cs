using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using InterfocusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Type;

namespace InterfocusAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class DividaController : ControllerBase
    {
        private readonly DividaService dividaService;
        public DividaController(DividaService dividaService)
        {
            this.dividaService = dividaService;
        }
        [HttpGet("dividas/{id}")]
        public async Task<IActionResult> Dividas(int id)
        {
            try
            {
                var dividas = await dividaService.VerDividas(id);
                return Ok(dividas);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("sumdividas/{id}")]
        public async Task<IActionResult> VerSomaDividas(int id)
        {
            try
            {
                var somadividas = await dividaService.SomaDividasCliente(id);
                return Ok(somadividas);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPost("dividas/{id}")]
        public async Task<IActionResult> CadastroDivida(int id, [FromBody] Divida divida)
        {
            try
            {
                var d = await dividaService.CadastrarDivida(id, divida);
                return Ok(d);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
            
        }
        [HttpPut("dividas/{id}")]
        public async Task<IActionResult> MarcarPaga(int id)
        {
            try
            {
                var divida = await dividaService.MarcarDividaPaga(id);
                return Ok(divida);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpDelete("dividas/{id}")]
        public async Task<IActionResult> DeletarDivida(int id)
        {
            try
            {
                var divida = await dividaService.RemoverDivida(id);
                return Ok(divida);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);  
            }
        }
    }
}
