using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection() : base("Conn")
        {
        }

        public DbSet<ExCategory> Model_Category { get; set; }
        public DbSet<Expenss> Model_Expense { get; set; }
    }
    public class ViewModel
    {
        public ExCategory exCategory { get; set; }
        public Expenss expense { get; set; }
    }
   

}