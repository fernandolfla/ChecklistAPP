using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

    }
}
