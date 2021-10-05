using System.ComponentModel.DataAnnotations;

namespace Locadora.DTO.Locacao
{
    public class DTORegistrarLocacao
    {
        [Required]
        public string NomeFilme { get; set; }
        [Required]
        public string NomeCliente { get; set; }
        [Required]
        public int CodigoFilme { get; set; }
    }
}
