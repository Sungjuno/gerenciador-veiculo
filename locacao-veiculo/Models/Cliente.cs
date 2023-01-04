using System;
using System.Collections.Generic;

namespace locacao_veiculo
{
    public partial class Cliente
    {
        public Cliente()
        {
            Carros = new HashSet<Carro>();
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Telefone { get; set; }
        public string Endereco { get; set; } = null!;

        public virtual ICollection<Carro> Carros { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
