using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var massTransitSession = builder.Configuration.GetSection("MassTransit");
var servidor = massTransitSession["servidor"];
var usuario = massTransitSession["usuario"];
var senha = massTransitSession["senha"];

//Configurando o MassTransit
builder.Services.AddMassTransit(settings =>
{
    //Indicando que o RabbitMQ ser� o Message Broker usado
    settings.UsingRabbitMq((context, config) =>
    {
        config.ExchangeType = "direct";

        //Configurando o servidor da fila, informando o endere�o que est� sendo usado, mais a separa��o,
        //mais as configura��es de acesso, passando o usuario e a senha.
        config.Host(servidor, "/", hostSettings =>
        {
            hostSettings.Username(usuario);
            hostSettings.Password(senha);
        });

        config.ConfigureEndpoints(context);

    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
