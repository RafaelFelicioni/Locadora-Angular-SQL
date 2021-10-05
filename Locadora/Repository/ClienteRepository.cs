using Locadora.Data;
using Locadora.DTO.Cliente;
using Locadora.Models;
using Locadora.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public APILocadoraContext _apiDesafioContext { get; set; }

        public ClienteRepository(APILocadoraContext aPIDesafioContext)
        {
            _apiDesafioContext = aPIDesafioContext;
        }

        public async Task<string> RegistrarCliente(DTORegistrarCliente cliente)
        {
            var resposta = "";

            var clientes = await _apiDesafioContext.Clientes.ToListAsync();

            if (clientes.Any(x => x.Nome == cliente.Nome))
            {
                resposta = "Este cliente ja está cadastrado no banco de dados";
                return resposta;
            }

            _apiDesafioContext.Clientes.Add(new ModelCliente()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone
            });

            _apiDesafioContext.SaveChanges();
            resposta = "Cliente cadastrado com sucesso";

            return resposta;
        }

        public async Task<string> AtualizarCliente(DTOAtualizarCliente cliente)
        {
            var resposta = "";
            var clientes = await _apiDesafioContext.Clientes.Where(x => x.IdCliente == cliente.IdCliente).ToListAsync();
            var entidadeCliente = clientes.FirstOrDefault();

            if (ValidarAtualizacaoCliente(clientes, cliente, ref resposta))
            {
                var clienteParaAtualizar = new ModelCliente()
                {
                    IdCliente = cliente.IdCliente,
                    Ativo = cliente.Ativo,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone
                };

                _apiDesafioContext.Entry(entidadeCliente).CurrentValues.SetValues(clienteParaAtualizar);
                _apiDesafioContext.SaveChanges();
                resposta = "Cliente atualizado com sucesso";
            }

            return resposta;
        }

        private bool ValidarAtualizacaoCliente(List<ModelCliente> clientes, DTOAtualizarCliente cliente, ref string resposta)
        {
            var validacaoCliente = clientes.Any(x => x.IdCliente != cliente.IdCliente && x.Nome == cliente.Nome);
            var entidadeCliente = clientes.FirstOrDefault();

            if (validacaoCliente)
            {
                resposta = "Este nome ja está cadastrado.";
                return false;
            }

            if (entidadeCliente == null)
            {
                resposta = "Este Cliente não existe no banco de dados";
                return false;
            }

            return true;
        }

        public async Task<List<DTORetornoClientes>> ObterTodosClientesDB()
        {
            var resposta = new List<DTORetornoClientes>();
            var clientes = await _apiDesafioContext.Clientes.ToListAsync();

            if (clientes.Count > 0)
            {
                foreach (var item in clientes)
                {
                    resposta.Add(new DTORetornoClientes()
                    {
                        Ativo = item.Ativo,
                        Email = item.Email,
                        IdCliente = item.IdCliente,
                        Nome = item.Nome,
                        Telefone = item.Telefone
                    });
                }
            }

            return resposta;
        }

    }

}
