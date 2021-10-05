using System.ComponentModel.DataAnnotations;

namespace Locadora.DTO.Cliente
{
    public class DTORegistrarCliente
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
    }
}
