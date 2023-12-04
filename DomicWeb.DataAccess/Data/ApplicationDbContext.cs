using DomicWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DomicWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        //uvijek se to mora dodat, sve konfiguracije koje dodamo želimo proslijediti te opcije bazi(baza je DbContext)
        //nakon toga želimo registrirati naš DbContext, to radimo u Program.cs
        //tu radimo sve vezano za našu bazu podataka
        //kada želimo dodati tablice moramo dodati DbSet
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } //prvo je klasa<> koju tablicu želimo dodati, drugo naziv tablice

        protected override void OnModelCreating(ModelBuilder modelBuilder) //već postoji u DbContext zato mora ić override
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action", DisplayOrder=1 },
                 new Category { Id=2, Name="SciFi", DisplayOrder=2 },
                  new Category { Id=3, Name="History", DisplayOrder=3 }
                ); //koristimo za seedanje podataka u tablicu Category
        }
    }
}
