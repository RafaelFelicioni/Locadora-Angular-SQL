using Locadora.DTO.Cliente;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<string> RegistrarCliente(DTORegistrarCliente dto);
        Task<string> AtualizarCliente(DTOAtualizarCliente dto);
        Task<List<DTORetornoClientes>> ObterTodosClientesDB();
    }
}
