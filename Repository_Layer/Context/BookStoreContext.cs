using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Layer.Entity;

namespace Repository_Layer.Context
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserRegistrationEntity> UsersRegistration { get; set; }

        public DbSet<BookEntity> Books { get; set; }
    }
}
