using Consumidor;
using Consumidor.Eventos;
using MassTransit;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var massTransitSession = hostContext.Configuration.GetSection("MassTransit");

        var nomeFila = massTransitSession["nomeFila"];
        var servidor = massTransitSession["servidor"];
        var usuario = massTransitSession["usuario"];
        var senha = massTransitSession["senha"];

        services.AddHostedService<Worker>();

        services.AddMassTransit(definicoes =>
        {
            definicoes.UsingRabbitMq((context, definicao) =>
            {
                //Configurando o servidor da fila, informando o endereço que está sendo usado, mais a separação,
                //mais as configurações de acesso, passando o usuario e a senha.
                definicao.Host(servidor, "/", hostSettings =>
                {
                    hostSettings.Username(usuario);
                    hostSettings.Password(senha);
                });

                definicao.ReceiveEndpoint(nomeFila, func =>
                {
                    func.Consumer<PedidoCriadoConsumer>();
                });

                definicao.ConfigureEndpoints(context);

            });

            definicoes.AddConsumer<PedidoCriadoConsumer>();

        });
    })
    .Build();

await host.RunAsync();
