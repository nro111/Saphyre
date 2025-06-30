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

        public static readonly string Schema = "dbo";

        public SaphyreContext(DbContextOptions<SaphyreContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Get the connection string from appsettings.json
                var connectionString = _configuration.GetConnectionString("");

                // Use Npgsql with the connection string
                //optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

        }  
            

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
