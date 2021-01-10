using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesWebAPI.Entity
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        [MinLength(3, ErrorMessage = "O nome deve ter um tamanho mínimo 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome.")]
        [MinLength(3, ErrorMessage = "O sobrenome deve ter um tamanho mínimo 3 caracteres")]
        public string Sobrenome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o email." )]
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha.")]
        [MinLength(6, ErrorMessage = "A senha deve ter um tamanho mínimo 6 caracteres")]
        public string Senha { get; set; }

        public List<Telefone> Telefones { get; set; }
    }
}
