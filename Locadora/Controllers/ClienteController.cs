using Locadora.DTO.Cliente;
using Locadora.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPut("RegistrarCliente", Name = "RegistrarCliente")]
        public async Task<string> RegistrarClientes(DTORegistrarCliente cliente)
        {
            var resposta = await _clienteRepository.RegistrarCliente(cliente);

            return resposta;
        }

        [HttpPost("AtualizarCliente", Name = "AtualizarCliente")]
        public async Task<string> UpdateCliente(DTOAtualizarCliente cliente)
        {
            var resposta = await _clienteRepository.AtualizarCliente(cliente);

            return resposta;
        }

        [HttpGet("ListaDeClientes", Name = "ListaDeClientes")]
        public async Task<List<DTORetornoClientes>> ListarClientes()
        {
            var resposta = new List<DTORetornoClientes>();
            resposta = await _clienteRepository.ObterTodosClientesDB();
            if (resposta.Count == 0) 
            {
                resposta.Add(new DTORetornoClientes()
                {
                    Erro = "Não existem clientes registrados no banco"
                });
            }

            return resposta;
        }
    }
}
