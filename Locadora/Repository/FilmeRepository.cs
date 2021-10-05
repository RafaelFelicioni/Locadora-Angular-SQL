using Locadora.Data;
using Locadora.DTO.Filme;
using Locadora.Models;
using Locadora.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        public APILocadoraContext _apiDesafioContext { get; set; }

        public FilmeRepository(APILocadoraContext aPIDesafioContext)
        {
            _apiDesafioContext = aPIDesafioContext;
        }

        public async Task<string> RegistrarFilme(DTORegistrarFilme filme)
        {
            var filmes = await _apiDesafioContext.Filmes.ToListAsync();
            var validacaoFilme = filmes.Any(x => x.CodigoFilme == filme.CodigoFilme);
            var resposta = "";

            if (string.IsNullOrEmpty(filme.Nome))
            {
                resposta = "Por favor informe o nome do filme a ser adicionado.";
                return resposta;
            }

            if (filme.CodigoFilme == 0)
            {
                resposta = "Por favor informe o código do filme.";
                return resposta;
            }

            if (validacaoFilme)
            {
                resposta = "Código informado ja está cadastrado no sistema, por favor informe um código válido.";
                return resposta;
            }

            _apiDesafioContext.Filmes.Add(new ModelFilme()
            {
                Nome = filme.Nome,
                CodigoFilme = filme.CodigoFilme
            });

            _apiDesafioContext.SaveChanges();

            resposta = "Filme adicionado com sucesso.";

            return resposta;
        }

        public async Task<string> AtualizarFilme(DTOAtualizarFilme filme)
        {
            var resposta = "";
            var filmes = await _apiDesafioContext.Filmes.Where(x => x.idFilme == filme.idFilme).ToListAsync();
            var filmeEntidade = filmes.FirstOrDefault();

            if (filmeEntidade == null)
            {
                resposta = "Este filme não existe no banco de dados";
                return resposta;
            }

            if (filme.Ativo == false && filmeEntidade.Disponivel == false)
            {
                resposta = "Não é possível desativar este filme no momento, pois ele esta alugado, aguarde a devolução e tente novamente.";
                return resposta;
            }

            var filmeParaAtualizar = new ModelFilme()
            {
               idFilme = filme.idFilme,
               Ativo = filme.Ativo,
               CodigoFilme = filme.CodigoFilme,
               Disponivel = filme.Disponivel,
               Nome = filme.Nome
            };

            _apiDesafioContext.Entry(filmeEntidade).CurrentValues.SetValues(filmeParaAtualizar);
            _apiDesafioContext.SaveChanges();
            resposta = "Filme atualizado com sucesso";

            return resposta;
        }

        public async Task<List<DTORetornoFilmes>> ObterTodosFilmes()
        {
            var resposta = new List<DTORetornoFilmes>();
            var filmes = await _apiDesafioContext.Filmes.ToListAsync();

            if (filmes.Count > 0)
            {
                foreach (var item in filmes)
                {
                    resposta.Add(new DTORetornoFilmes()
                    {
                        idFilme = item.idFilme,
                        CodigoFilme = item.CodigoFilme,
                        Disponivel = item.Disponivel,
                        Ativo = item.Ativo,
                        Nome = item.Nome
                    });
                }
            }
            else
            {
                resposta.Add(new DTORetornoFilmes()
                {
                    erro = "Não existem filmes cadastrados no banco."
                });
            }

            return resposta;
        }
    }
}
