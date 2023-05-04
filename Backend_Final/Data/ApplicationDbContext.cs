using Microsoft.EntityFrameworkCore;
using Backend_Final.Models;

namespace Backend_Final.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Agregar los modelos aqui
        public DbSet<bicicleta> Bicicleta { get; set; }
        public DbSet<usuario> Usuario { get; set; }

    }
}
