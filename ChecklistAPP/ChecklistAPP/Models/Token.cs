using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public bool logado { get; set; }
        public int FilialId { get; set; }
        public Usuario Usuario { get; set; }
        public string Mensagem { get; set; }

    }
}
