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
        
        public DbSet<Investment> investments{get;set;}
    }
}