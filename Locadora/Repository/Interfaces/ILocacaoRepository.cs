using Locadora.DTO.Locacao;
using System.Threading.Tasks;

namespace Locadora.Repository.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<string> registrarLocacao(DTORegistrarLocacao dto);
        Task<string> registrarDevolucao(DTORegistrarDevolucao dto);
    }
}
