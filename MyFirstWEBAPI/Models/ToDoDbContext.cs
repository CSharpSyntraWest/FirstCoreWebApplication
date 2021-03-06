﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyFirstWEBAPI.Models
{
    public class ToDoDbContext : DbContext
    {
        private IConfigurationRoot configuration;
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json", false)
                    .Build();
            string connectionString = configuration.GetConnectionString("ToDosConnection");
            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<ToDo>(toDo =>
            {
                toDo.Property(c => c.Id)
                            .ValueGeneratedOnAdd(); //  autonummering
                toDo.HasKey(s => s.Id);
                toDo.Property(s => s.Titel).IsRequired().HasMaxLength(50);
                toDo.Property(s => s.Omschrijving).IsRequired().HasMaxLength(255);

                //toDo.HasOne(t => t.User)
                //        .WithMany(u => u.ToDos)
                //        .HasForeignKey(t => t.UserId);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Id).ValueGeneratedOnAdd();
                user.HasKey(u => u.Id);
                user.Property(u => u.VoorNaam).IsRequired().HasMaxLength(20); //wordt dan nvarchar(20)
                user.Property(u => u.FamilieNaam).IsRequired().HasMaxLength(20);
                user.HasMany(u => u.ToDos);
                //user.HasMany(u => u.ToDos)
                //        .WithOne(u => u.User)
                //        .HasForeignKey(t => t.UserId);

            });
            base.OnModelCreating(modelBuilder);
        }
      
    }
}