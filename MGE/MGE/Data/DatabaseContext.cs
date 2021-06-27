using MGE.Models.Categorias;
using MGE.Models.Itens;
using MGE.Models.Parametros;
using Microsoft.EntityFrameworkCore;

namespace MGE.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ParametrosEntity> Parametros { get; set; }
        public DbSet<ItensEntity> Itens { get; set; }
        public DbSet<CategoriasEntity> Categorias { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
