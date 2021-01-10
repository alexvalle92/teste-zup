using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesWebAPI.Entity
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o número de telefone.")]
        [RegularExpression("\\(?\\d{2}\\)?-? *\\d{4,5}-? *-?\\d{4}", ErrorMessage = "Valor inválido para telefone. Deve possuir seguinte estrutura (99)99999-9999")]
        public string NumeroTelefone { get; set; }
        
    }
}
