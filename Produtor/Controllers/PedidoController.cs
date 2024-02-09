using Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Produtor.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {

        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public PedidoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var sessaoMassTransit = _configuration.GetSection("MassTransit");

            //Obtendo o nome da fila definida
            var nomeFila = sessaoMassTransit["nomeFila"] ?? null;

            //Com o nome da fila e o endereço definido na configuração, passar no metodo o nome da fila retornado, com o padrão queue: nomeDaFila
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

            //Enviando mensagem para o Message Broker (nesse caso, o RabbitMQ)
            await endpoint.Send(new Pedido(1, new Usuario(1, "Fernando", "fernando@gmail.com")));

            return Ok("Mensagem Enviada");
        }

    }
}
