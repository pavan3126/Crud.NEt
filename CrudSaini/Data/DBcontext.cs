using CrudSaini.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudSaini.Data
{
    public class DBcontext:DbContext
    {
        
        public DBcontext(DbContextOptions options): base(options) { 
        
        
        }
        //the table name will become employees
        public DbSet< Employee> Employees { get; set; }


    }
}
