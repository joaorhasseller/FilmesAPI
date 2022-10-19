using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext // Para dizer que FilmeContex é um contexto de comuniação da Api para o banco de dados
    {

        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt)               // Vai passar o que nós quremos criar, as opções, quais são as opção desse contexo
        {

        }



        public DbSet<Filme> Filmes { get; set; }  // Conjunto de dados do BANCO, ENCAPSUALMENTO DO ACESSO AOS DADOS DO BANCO DE DADOS   

                                                  //  Vai ser do Filme, o objete que queremos mapear, acessar dentro do banco de dados; isso é como se fosse acesso a uma tabela do banco de daos

      


    }




}
