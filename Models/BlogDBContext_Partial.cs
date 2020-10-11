using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Models
{
    public partial class BlogDBContext : DbContext
    {
        public virtual DbSet<Sp_GetAllPost> Sp_GetAllPost { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sp_GetAllPost>().HasNoKey();
        }
    }
}
 
