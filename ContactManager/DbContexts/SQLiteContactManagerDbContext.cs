using ContactManager.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.DbContexts
{
    public class SQLiteContactManagerDbContext : ContactManagerDbContext
    {
        public SQLiteContactManagerDbContext(DbContextOptions options) : base(options)
        {

        }        
    }
}
