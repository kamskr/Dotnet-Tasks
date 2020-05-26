using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class DoctorDbContext: DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DoctorDbContext()
        {

        }

        public DoctorDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();

                entity.ToTable("Doctor");

                entity.HasMany(d => d.Prescriptions)
                      .WithOne(p => p.Doctor)
                      .HasForeignKey(p => p.IdDoctor)
                      .IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);

                entity.ToTable("Prescription");

            });


        }
    }
}
