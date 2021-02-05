using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyFirstMVCWebApp.Models
{
    public partial class BierenDbContext : DbContext
    {
        private static IConfigurationRoot configuration;
        public BierenDbContext()
        {
        }

        public BierenDbContext(DbContextOptions<BierenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bieren> Bieren { get; set; }
        public virtual DbSet<Brouwers> Brouwers { get; set; }
        public virtual DbSet<Soorten> Soorten { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                optionsBuilder.UseSqlServer("....");
            //            }
            configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                .AddJsonFile("appsettings.json", false)
                                .Build();
            string connectionString = configuration.GetConnectionString("BierenDbConnection");
            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bieren>(entity =>
            {
                entity.HasKey(e => e.BierNr);

                entity.Property(e => e.BierNr).ValueGeneratedNever();

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrouwerNrNavigation)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.BrouwerNr)
                    .HasConstraintName("FK_Bieren_Brouwers");

                entity.HasOne(d => d.SoortNrNavigation)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.SoortNr)
                    .HasConstraintName("FK_Bieren_Soorten");
            });

            modelBuilder.Entity<Brouwers>(entity =>
            {
                entity.HasKey(e => e.BrouwerNr);

                entity.Property(e => e.BrouwerNr).ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gemeente)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Soorten>(entity =>
            {
                entity.HasKey(e => e.SoortNr);

                entity.Property(e => e.SoortNr).ValueGeneratedNever();

                entity.Property(e => e.Soort)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

 

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
