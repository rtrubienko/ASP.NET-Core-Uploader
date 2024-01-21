using Microsoft.EntityFrameworkCore;
using TestApp.Data.Models;


namespace TestApp.Data
{
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options)
        {

        }

        public DbSet<UploadJson> UploadJson { get; set; }
    }
}
