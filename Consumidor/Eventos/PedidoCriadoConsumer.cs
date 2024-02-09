using Core;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumidor.Eventos
{
    public class PedidoCriadoConsumer : IConsumer<Pedido>
    {
        public async Task Consume(ConsumeContext<Pedido> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
