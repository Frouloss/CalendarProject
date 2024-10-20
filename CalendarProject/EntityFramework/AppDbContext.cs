﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarProject.EntityFramework
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Settings> Settings { get; set; }

        private readonly string path;

        public AppDbContext(string path)
        {
            this.path = path;
#if DEBUG
            if (File.Exists(path)) File.Delete(path);
#endif
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={path}");
        }
    }
}