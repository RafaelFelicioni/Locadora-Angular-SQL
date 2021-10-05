using Locadora.Data;
using Locadora.DTO.Locacao;
using Locadora.Models;
using Locadora.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
    public class LocacaoRepository : ILocacaoRepository
    {
        public APILocadoraContext _apiDesafioContext { get; set; }

        public LocacaoRepository(APILocadoraContext aPIDesafioContext)
        {
            _apiDesafioContext = aPIDesafioContext;
        }

        public async Task<string> registrarLocacao(DTORegistrarLocacao dto)
        {
            var resposta = "";
            var clientes = await _apiDesafioContext.Clientes.Where(x => x.Nome == dto.NomeCliente).ToListAsync();
            var cliente = clientes.FirstOrDefault();
            var filmes = await _apiDesafioContext.Filmes.ToListAsync();
            var filme = filmes.FirstOrDefault(x => x.CodigoFilme == dto.CodigoFilme && x.Nome == dto.NomeFilme);

            if (ValidarLocacao(clientes ,filmes, dto, ref resposta))
            {
                filme.Disponivel = false;
                _apiDesafioContext.Locacoes.Add(new ModelLocacao()
                {
                    CodigoFilme = dto.CodigoFilme,
                    DataLocacao = DateTime.Now,
                    DataMaxDevolucao = DateTime.Now.AddDays(7),
                    IdCliente = cliente.IdCliente,
                    IdFilme = filme.idFilme
                });

                _apiDesafioContext.SaveChanges();
                resposta = "Locação adicionada com sucesso!";
            }

            return resposta;
        }

        private bool ValidarLocacao(List<ModelCliente> clientes, List<ModelFilme> filmes, DTORegistrarLocacao dto, ref string resposta)
        {
            var cliente = clientes.FirstOrDefault();
            var filme = filmes.FirstOrDefault(x => x.CodigoFilme == dto.CodigoFilme && x.Nome == dto.NomeFilme);
            var validacaoDisponivel = filmes.Where(x => x.CodigoFilme == dto.CodigoFilme).Select(x => x.Disponivel).FirstOrDefault();

            if (filme == null)
            {
                resposta = "Não foi possível encontrar no banco a combinação de filme e código, por favor informe um nome e código válidos.";
                return false;
            }

            if (filme.Ativo == false)
            {
                resposta = "Este filme esta desativado, ative o filme e tente novamente.";
                return false;
            }

            if (!validacaoDisponivel && filme != null)
            {
                resposta = "Não foi possível concluir a locação do filme, pois o filme informado não está disponível.";
                return false;
            }

            if (cliente == null || cliente.Ativo == false)
            {
                resposta = "Não foi possível concluir a locação do filme, pois o cliente informado não existe no banco de dados ou esta desativado.";
                return false;
            }

            return true;
        }

        public async Task<string> registrarDevolucao(DTORegistrarDevolucao dto)
        {
            var resposta = "";
            var clienteId = await _apiDesafioContext.Clientes.Where(x => x.Nome == dto.NomeCliente).Select(x => x.IdCliente).ToListAsync();
            var filmes = await _apiDesafioContext.Filmes.Where(x => x.CodigoFilme == dto.CodigoFilme && x.Nome == dto.NomeFilme).ToListAsync();
            var locacoes = await _apiDesafioContext.Locacoes.Where(x => x.CodigoFilme == dto.CodigoFilme && x.IdCliente == clienteId.FirstOrDefault()).ToListAsync();
            var filmeParaDevolver = filmes.FirstOrDefault();
            var identificacaoLocacao = locacoes.FirstOrDefault();

            if (filmeParaDevolver == null || identificacaoLocacao == null)
            {
                resposta = "Combinação de código, nome do cliente e nome do filme informado inválidos, por favor informar um código e nome válidos.";
                return resposta;
            }

            if (identificacaoLocacao.DataMaxDevolucao > DateTime.Now)
            {
                resposta = "Filme devolvido com sucesso.";

                filmeParaDevolver.Disponivel = true;
                identificacaoLocacao.Ativo = false;
                identificacaoLocacao.DevolvidoEm = DateTime.Now;
                _apiDesafioContext.Locacoes.Update(identificacaoLocacao);
                _apiDesafioContext.SaveChanges();
            }
            else 
            {
                resposta = "Filme devolvido com sucesso, mas com atraso, portanto será aplicada multa!";

                filmeParaDevolver.Disponivel = true;
                identificacaoLocacao.Ativo = false;
                identificacaoLocacao.DevolvidoEm = DateTime.Now;
                _apiDesafioContext.Locacoes.Update(identificacaoLocacao);
                _apiDesafioContext.SaveChanges();
            }

            return resposta;
        }
    }
}
