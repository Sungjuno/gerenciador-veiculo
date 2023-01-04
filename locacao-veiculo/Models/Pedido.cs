using System;
using System.Collections.Generic;

namespace locacao_veiculo
{
    public partial class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Carro { get; set; } = null!;
        public DateTime DataLocacao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
    }
}
