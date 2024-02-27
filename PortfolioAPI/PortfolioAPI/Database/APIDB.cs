using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Database
{
    public class APIDB : DbContext
    {
        public APIDB(DbContextOptions<APIDB> options):base(options){

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Investment>()
                .HasKey(investments => investments.Id)
                .IsClustered(false);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id)
                .IsClustered(false);

            modelBuilder.Entity<Investment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Investment> investments{get;set;}
        public DbSet<User> users{get;set;}
    }
}