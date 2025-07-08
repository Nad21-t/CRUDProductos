using CRUDProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDProductos.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Producto> Producto { get; set; } = default!;

    }
}
