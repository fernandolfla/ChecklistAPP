using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Check_Item
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int CheckId { get; set; }
        public Check Check { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public bool Selecionado { get; set; }

    }
}
