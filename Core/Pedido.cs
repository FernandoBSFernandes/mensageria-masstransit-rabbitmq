namespace Core
{
    public class Pedido
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        public static DateTime DataPedido { get { return DateTime.Now; } }

        public Pedido(int id, Usuario usuario)
        {
            Id = id;
            Usuario = usuario;
        }

        public override string ToString() => $"ID do Pedido: {Id}, Usuário {Usuario.Nome}, criado em {DataPedido:dd/MM/yyyy}.";

    }
}