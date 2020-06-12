
namespace ChecklistAPP.Models
{
    public class Chave
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }

    }
}
