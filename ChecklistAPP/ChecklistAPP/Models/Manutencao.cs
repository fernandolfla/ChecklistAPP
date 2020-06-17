using System;

namespace ChecklistAPP.Models
{
    public class Manutencao
    {
        public Manutencao()
        {
            Ativo = true;
        }
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int Km { get; set; }
        public string Servico { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public double Valor { get; set; }
        public byte[] Foto_Nota { get; set; }
        public string Foto_string { get; set; }
        public int FilialId { get; set; }
        public Filial Filial { get; set; }


    }
}
