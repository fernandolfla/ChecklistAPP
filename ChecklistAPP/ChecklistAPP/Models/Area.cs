
namespace ChecklistAPP.Models
{
    public class Area
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public int Areatatico { get; set; }
        public int FilialId { get; set; }
        public Filial Filial { get; set; }
    }
}
