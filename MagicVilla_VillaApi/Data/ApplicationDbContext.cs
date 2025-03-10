using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Villa> Villas { get; set; }

    }
}
