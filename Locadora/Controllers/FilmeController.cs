using Locadora.DTO.Filme;
using Locadora.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpPut("RegistrarFilme", Name = "RegistrarFilme")]
        public async Task<string> registrarFilmeDb(DTORegistrarFilme dto)
        {
            var resposta = await _filmeRepository.RegistrarFilme(dto);

            return resposta;
        }

        [HttpPost("AtualizarFilme", Name = "AtualizarFilme")]
        public async Task<string> AtualizarFilme(DTOAtualizarFilme filme)
        {
            var resposta = await _filmeRepository.AtualizarFilme(filme);

            return resposta;
        }

        [HttpGet("FilmesLista", Name = "FilmesLista")]
        public async Task<List<DTORetornoFilmes>> obterTodosFilmesDB()
        {
            var resposta = new List<DTORetornoFilmes>();
            resposta = await _filmeRepository.ObterTodosFilmes();

            return resposta;
        }
    }
}
