using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Problema
    {
        //Implementação Futura
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int VeículoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public bool Resolvido { get; set; }
        public byte[] Foto_antes { get; set; }
        public byte[] Foto_depois { get; set; }



    }
}
