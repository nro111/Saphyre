using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public partial class SaphyreContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public static readonly string Schema = "public";

        public SaphyreContext(DbContextOptions<SaphyreContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

        }  
            

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
