using Microsoft.EntityFrameworkCore;
using HolidayApi.Models;

namespace HolidayApi.Data
{
    public class HolidayDbContext : DbContext
    {
        public HolidayDbContext(DbContextOptions<HolidayDbContext> options) : base(options)
        {
        }
        public DbSet<HolidayModel> Feriados { get; set; }
    }
}
