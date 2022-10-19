using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context; // Para inicializar isso, deve ser via CONSTRUTOR. Para inicializar a Conexão com o banco de dados.

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost] // Verbo POST é reconhecido por criar um recurso novo, no sistema, 
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {

            Filme filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,
                Diretor = filmeDto.Diretor,



            };
           
           _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);
            
        }

        [HttpGet]
        public IEnumerable<Filme> RecupeparFilme()
        {
            return _context.Filmes; // Recupera toda a lista de filmes.
        }


        [HttpGet("{id}")] // Irá pegar o valor de id quer iremos passar via parâmetro na url e irá colocar na função abaixo.
        public IActionResult RecuperaFilmePorId(int id)
        {
          Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); // Para cada filme, quero que devolva o filme com id == id.

            if (filme != null)
            {

                ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Id = filme.Id,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now,
                };
                return Ok(filmeDto);
            }

            return NotFound();
        }


        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if(filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeDto.Titulo;
            filme.Diretor = filmeDto.Diretor;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme.Id == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
