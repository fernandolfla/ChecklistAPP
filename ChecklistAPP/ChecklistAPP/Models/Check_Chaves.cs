using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Check_Chaves
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int ChaveId { get; set; }
        public Chave Chave { get; set; }
        public int CheckId { get; set; }
        public Check Check { get; set; }
        public bool Selecionado { get; set; }

    }
}
