using Locadora.DTO.Locacao;
using Locadora.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public LocacaoController(ILocacaoRepository locacaoRepository) {
            _locacaoRepository = locacaoRepository;
        }

        [HttpPut("RegistrarLocacao", Name = "RegistrarLocacao")]
        public async Task<string> registrarLocacaoDB(DTORegistrarLocacao dto)
        {
            var resposta = await _locacaoRepository.registrarLocacao(dto);

            return resposta;
        }

        [HttpPut("RegistrarDevolucao", Name = "RegistrarDevolucao")]
        public async Task<string> registrarDevolucaoDB(DTORegistrarDevolucao dto)
        {
            var resposta = await _locacaoRepository.registrarDevolucao(dto);

            return resposta;
        }
    }
}
