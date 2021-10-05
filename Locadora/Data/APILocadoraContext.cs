using Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Data
{
    public class APILocadoraContext : DbContext
    {
        public DbSet<ModelCliente> Clientes { get; set; } 
        public DbSet<ModelFilme> Filmes { get; set; } 
        public DbSet<ModelLocacao> Locacoes { get; set; } 

        public APILocadoraContext(DbContextOptions<APILocadoraContext> opts) : base(opts)
        {

        }
    }
}
