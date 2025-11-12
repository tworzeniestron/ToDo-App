using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MettecApi.Models;

namespace MettecApi.Data
{

    public class MettecContext(DbContextOptions<MettecContext> options) : DbContext(options)
    {
        public DbSet<MettecItem> Todos { get; set; }
    }
}
