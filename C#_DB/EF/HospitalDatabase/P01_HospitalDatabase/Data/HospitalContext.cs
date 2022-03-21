using Microsoft.EntityFrameworkCore;

using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    class HospitalContext:DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext( DbContextOptions options) 
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.Configuration.ConnectionString);
            }
        }
        public virtual DbSet<Diagnose> Diagnoses { get; set; }
        public virtual DbSet<Medicament> Medicaments{ get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visitation> Visitations { get; set; }
        public virtual DbSet<PatientMedicament> PatientMedicaments { get; set; }  
        public virtual DbSet<Doctor> Doctors { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId });


                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.MedicamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
