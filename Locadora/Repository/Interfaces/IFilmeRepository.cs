using Locadora.DTO.Filme;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Repository.Interfaces
{
    public interface IFilmeRepository
    {
        Task<string> RegistrarFilme(DTORegistrarFilme dto);
        Task<List<DTORetornoFilmes>> ObterTodosFilmes();
        Task<string> AtualizarFilme(DTOAtualizarFilme filme);
    }
}
