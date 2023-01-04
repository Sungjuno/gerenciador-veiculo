using System;
using System.Collections.Generic;

namespace locacao_veiculo
{
    public partial class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
    }
}
