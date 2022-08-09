using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LINQ.Models;

namespace LINQ
{
   public class TableContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Studenter { get; set; }
        public DbSet<Kurs> Kurser { get; set; }
        public DbSet<Klass> Klasser { get; set; }

        public DbSet<StudentKurs> StudentKurser { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
               @"Data source = FRIGO\localdb;Initial Catalog=Linq;Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentKurs>()
                .HasOne(s => s.Student)
                .WithMany(sc => sc.StudentKurser)
                .HasForeignKey(si => si.StudentID);

            modelBuilder.Entity<StudentKurs>()
                .HasOne(s => s.Kurs)
                .WithMany(sc => sc.Studentkurser)
                .HasForeignKey(ci => ci.KursId);

        }
    }
}
