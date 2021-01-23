using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DapperInMVC.Models;
namespace DapperInMVC
{
    public class ApllicationDbContext:DbContext
    {
        public ApllicationDbContext() { }   
        public ApllicationDbContext(DbContextOptions<ApllicationDbContext> options) : base(options) { }
        public DbSet<DapperInMVC.Models.Employee> Employee { get; set; }
    
    }
}
