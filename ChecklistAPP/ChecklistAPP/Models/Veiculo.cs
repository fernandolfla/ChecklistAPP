using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Km { get; set; }
        public int Ultimatroca { get; set; }
        public int Proximatroca { get; set; }
        public int Trocanormal { get; set; }
        public int Situacao { get; set; }
        public int FilialId { get; set; }
        public Filial Filial { get; set; }

    }
}
