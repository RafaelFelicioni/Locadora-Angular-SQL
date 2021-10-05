using Locadora.Data;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Business
{
    public class AdicionarDados
    {
        public static void AdicionarDadosAoInicializar(APILocadoraContext contexto) {
            var listaClientes = new List<ModelCliente>();
            var listaFilmes = new List<ModelFilme>();

            listaClientes.Add(new ModelCliente() {
                Nome = "Rafael",
                Email = "rafaelalmeida1995@outlook.com",
                Telefone = "2121-2121"
            });

            listaClientes.Add(new ModelCliente()
            {
                Nome = "João",
                Email = "JoãoVicente@gmail.com",
                Telefone = "2222-2222"
            });

            listaFilmes.Add(new ModelFilme() {
                Nome = "Harry Potter e a Câmara Secreta",
                CodigoFilme = 1
            });

            listaFilmes.Add(new ModelFilme() {
                Nome = "Senhor Dos Anéis: As Duas Torres",
                CodigoFilme = 2
            });

            contexto.Clientes.AddRange(listaClientes);
            contexto.Filmes.AddRange(listaFilmes);
            contexto.SaveChanges();
        }
    }
}
