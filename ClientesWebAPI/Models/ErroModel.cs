using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesWebAPI.Models
{
    public class ErroModel
    {
        public string Mensagem { get; set; }
        public string StackTrace { get; set; }
    }
}
