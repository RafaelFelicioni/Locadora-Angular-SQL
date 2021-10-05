using System.ComponentModel.DataAnnotations;

namespace Locadora.DTO.Filme
{
    public class DTORegistrarFilme
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public int CodigoFilme { get; set; }
    }
}
