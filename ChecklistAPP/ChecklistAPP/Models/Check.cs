using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Check
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public DateTime Datainicio { get; set; }
        public DateTime Datafim { get; set; }
        public int Kminicial { get; set; }
        public int Kmfinal { get; set; }
        public string Obs { get; set; }
        public bool Encerrado { get; set; }
        public int FilialId { get; set; }
        public Filial Filial { get; set; }
        public List<Check_Item> Items { get; set; }
        public List<Check_Chaves> Chaves { get; set; }
        public List<Check_Foto> Fotos { get; set; }



    }
}
