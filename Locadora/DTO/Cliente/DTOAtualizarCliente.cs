using System.ComponentModel.DataAnnotations;

namespace Locadora.DTO.Cliente
{
    public class DTOAtualizarCliente
    {
        [Required]
        public int IdCliente { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefone { get; set; } = "";
        public bool Ativo { get; set; }
    }
}
