using DataAccess.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class LabsDbContext : IdentityDbContext<IdentityUser>
    {
        public LabsDbContext(DbContextOptions<LabsDbContext> options): base(options)
        {

        }
        public DbSet<StudentEntity> StudentEntities { get; set; }                                                                    
        public DbSet<AssignmentEntity> AssignmentEntities { get; set; }
        public DbSet<LaboratoryEntity> LaboratoryEntities { get; set; }
        public DbSet<GradingEntity> GradingEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.; Database=LabActivity; Trusted_Connection=True;");
        }
    }
}
