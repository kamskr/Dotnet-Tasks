using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task11.Entities
{
    public class DoctorDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        public DoctorDbContext()
        {

        }

        public DoctorDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Build db entity
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

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();

                entity.ToTable("Patient");

                entity.HasMany(d => d.Prescriptions)
                      .WithOne(p => p.Patient)
                      .HasForeignKey(p => p.IdPatient)
                      .IsRequired();
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(a => a.IdMedicament).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Type).IsRequired();

                entity.ToTable("Medicament");

                entity.HasMany(d => d.Prescription_Medicament)
                      .WithOne(p => p.Medicament)
                      .HasForeignKey(p => p.IdMedicament)
                      .IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);

                entity.ToTable("Prescription");

                entity.HasMany(d => d.Prescription_Medicament)
                      .WithOne(p => p.Prescription)
                      .HasForeignKey(p => p.IdPrescription)
                      .IsRequired();

            });

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Details).IsRequired();
                entity.Property(e => e.Dose).IsRequired();

                entity.HasOne(d => d.Medicament).
                WithMany(p => p.Prescription_Medicament).
                HasForeignKey(d => d.IdMedicament).
                OnDelete(DeleteBehavior.ClientSetNull).
                HasConstraintName("PM_Medicament");

                entity.HasOne(d => d.Prescription).
                WithMany(p => p.Prescription_Medicament).
                HasForeignKey(d => d.IdPrescription).
                OnDelete(DeleteBehavior.ClientSetNull).
                HasConstraintName("PM_Prescription");
            });

            Seed(modelBuilder);
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 1, FirstName = "Kamil", LastName = "Sikora", BirthDate = DateTime.Parse("30-09-1998") },
                new Patient { IdPatient = 2, FirstName = "Janek", LastName = "Witkowski", BirthDate = DateTime.Parse("10-10-1999") },
                new Patient { IdPatient = 3, FirstName = "Maria", LastName = "Szwedo", BirthDate = DateTime.Parse("25-01-2000") },
                new Patient { IdPatient = 4, FirstName = "Krzysztof", LastName = "Górny", BirthDate = DateTime.Parse("01-09-1998") },
                new Patient { IdPatient = 5, FirstName = "Maciek", LastName = "Janiszewski", BirthDate = DateTime.Parse("25-01-1990") }
                );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 1, FirstName = "Marcin", LastName = "Polkowski", Email = "marcin.polkowski@gmail.com" },
                new Doctor { IdDoctor = 2, FirstName = "Mariusz", LastName = "Chrabąszcz", Email = "mariusz.ch12@gmail.com" },
                new Doctor { IdDoctor = 3, FirstName = "Konrad", LastName = "Cornel", Email = "cornel@gmail.com" }
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 1, Name = "Strepsils", Description = "Na ból gardła", Type = "Lek przeciwzapalny" },
                new Medicament { IdMedicament = 2, Name = "Cirrus", Description = "Na katar", Type = "Lek przeciwalergiczny" },
                new Medicament { IdMedicament = 3, Name = "Rutinoskorbin", Description = "Na odporność", Type = "Lek wzmacniający" }
                );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Parse("10-06-2020"), IdPatient = 1, IdDoctor = 2 },
                new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Parse("12-06-2020"), IdPatient = 2, IdDoctor = 2 },
                new Prescription { IdPrescription = 3, Date = DateTime.Now, DueDate = DateTime.Parse("12-07-2020"), IdPatient = 2, IdDoctor = 3 }
                );

            modelBuilder.Entity<Prescription_Medicament>().HasData(
                new Prescription_Medicament { Dose = 1, Details = "Stosować przez 2 tygodnie", IdMedicament = 1, IdPrescription = 1 },
                new Prescription_Medicament { Dose = 2, Details = "Stosować raz na 3 godziny", IdMedicament = 2, IdPrescription = 2 }
                );
        }
    }
}
