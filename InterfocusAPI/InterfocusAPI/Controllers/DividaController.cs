using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using InterfocusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Type;

namespace InterfocusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividaController : ControllerBase
    {
        private readonly DividaService dividaService;
        public DividaController(DividaService dividaService)
        {
            this.dividaService = dividaService;
        }
        [HttpGet("verdividas")]
        public IActionResult Dividas(int id)
        {
            var dividas = dividaService.VerDividas(id);
            return Ok(dividas);
        }
        [HttpGet("somadividas")]
        public IActionResult VerSomaDividas(int id)
        {
            var somadividas = dividaService.SomaDividasCliente(id);
            return Ok($"A soma das dívidas é de {somadividas} reais");
        }
        [HttpPost("cadastrardivida")]
        public IActionResult CadastroDivida(int id, [FromBody] Divida divida)
        {
            var d = dividaService.CadastrarDivida(id, divida);
            return Ok(d);
        }
        [HttpPut("atualizardivida")]
        public IActionResult MarcarPaga(int id, [FromBody] DividaDTO dividadto)
        {
            var divida = dividaService.MarcarDividaPaga(id, dividadto);
            return Ok(divida);
        }
        [HttpDelete("deletardivida")]
        public IActionResult DeletarDivida(int id)
        {
            var divida = dividaService.RemoverDivida(id);
            if (divida == null)
            {
                return NotFound();
            }
            return Ok(divida);
        }
    }
}
