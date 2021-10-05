namespace Locadora.DTO.Filme
{
    public class DTORetornoFilmes
    {
        public int idFilme { get; set; }
        public string Nome { get; set; }
        public int CodigoFilme { get; set; }
        public bool Disponivel { get; set; }
        public bool Ativo { get; set; }
        public string erro { get; set; }
    }
}
