using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JalgrattaEksamMVC.Models;

namespace JalgrattaEksamMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Eksam>()
                .HasIndex(e => new { e.Eesnimi, e.Perenimi })
                .IsUnique();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JalgrattaEksamMVC.Models.Eksam> Eksam { get; set; }
        public DbSet<JalgrattaEksamMVC.Models.LubaViewModel> LubaViewModel { get; set; }
    }
}
