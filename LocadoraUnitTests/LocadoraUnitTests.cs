using Locadora.Controllers;
using Locadora.DTO.Cliente;
using Locadora.Repository.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DesafioUnitTests
{
    public class LocadoraUnitTests
    {
        private readonly ClienteController _sut;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock = new Mock<IClienteRepository>();

        public LocadoraUnitTests()
        {
            _sut = new ClienteController(_clienteRepositoryMock.Object);
        }

        [Fact]
        public void ObterTodosClientesDB_DeveRetornarCliente_SeClienteExistir()
        {

            // Arrange
            var dtoCliente = new List<DTORetornoClientes>();
            var emailsDummy = new List<string>()
            {
                    "rafael@outlook.com", "teste@outlook.com"
            };
            var idsDummy = new List<int>
            {
                    1, 2
            };
            var nomesDummy = new List<string>
            {
                    "Rafael", "Teste"
            };
            var telefonesDummy = new List<string>
            {
                    "2121-2121", "2323-2323"
            };
            var ativoDummy = new List<bool>
            {
                    true, true
            };

            for (int i = 0; i < idsDummy.Count; i++)
            {
                dtoCliente.Add(new DTORetornoClientes()
                {
                    Email = emailsDummy[i],
                    IdCliente = idsDummy[i],
                    Ativo = ativoDummy[i],
                    Nome = nomesDummy[i],
                    Telefone = telefonesDummy[i],
                });
            }

            _clienteRepositoryMock.Setup(x => x.ObterTodosClientesDB().Result)
              .Returns(dtoCliente);

            // Act 
            var emails = _sut.ListarClientes().Result;

            // Assert
            Assert.Equal(dtoCliente, (IEnumerable<DTORetornoClientes>)emails);

        }

        [Fact]
        public void ObterTodosClientesDB_DeveRetornarErro_SeNaoExistir()
        {

            // Arrange
            var dtoClientes = new List<DTORetornoClientes>();

            _clienteRepositoryMock.Setup(x => x.ObterTodosClientesDB().Result)
                .Returns(dtoClientes);

            // Act 
            var emails = _sut.ListarClientes().Result;


            // Assert
            Assert.Equal("Não existem clientes registrados no banco", emails[0].Erro);
        }
    }
}
