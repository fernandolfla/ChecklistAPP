using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Abastecimento
    {
        public Abastecimento()
        {
            Ativo = true;

        }
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int UsuarioId { get; set; }  // Interrogação é para marcar chave estrangeira aceita nulo
        public Usuario Usuario { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public DateTime Data { get; set; }
        public int Km { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public double Valor { get; set; }
        public double Litros { get; set; }
        public byte[] Foto_Nota { get; set; }
        public string Foto_string { get; set; }
        public int FilialId { get; set; }
        public Filial Filial { get; set; }
    }
}
