using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.models
{
    public class ClinicDBContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        public ClinicDBContext()
        {

        }
        
        public ClinicDBContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription_Medicament>().HasKey(premed => new { premed.IdMedicament, premed.IdPrescription });
            modelBuilder.Entity<Prescription_Medicament>().HasOne(premed => premed.Medicament).WithMany(med => med.Prescription_Medicaments).HasForeignKey(premed => premed.IdMedicament);
            modelBuilder.Entity<Prescription_Medicament>().HasOne(premed => premed.Prescription).WithMany(pre => pre.Prescription_Medicaments).HasForeignKey(premed => premed.IdPrescription);

            modelBuilder.Entity<Doctor>().HasData(seedDoctorData());
            modelBuilder.Entity<Patient>().HasData(seedPatientData());
            modelBuilder.Entity<Medicament>().HasData(seedMedicamentData());
            modelBuilder.Entity<Prescription>().HasData(seedPrescriptionData());
            modelBuilder.Entity<Prescription_Medicament>().HasData(seedPreMedData());
        }

        private List<Patient> seedPatientData()
        {
            var generatedPatientData = new List<Patient>();
            generatedPatientData.Add(new Patient {IdPatient = 1, FirstName = "Tomasz", LastName = "Rachanski", Birthdate = DateTime.Parse("1998-06-16")});
            generatedPatientData.Add(new Patient {IdPatient = 2, FirstName = "Magdalena", LastName = "Nikoń", Birthdate = DateTime.Parse("1994-06-16")});
            return generatedPatientData;
        }

        private List<Medicament> seedMedicamentData()
        {
            var generatedMedicamentData = new List<Medicament>();
            generatedMedicamentData.Add(new Medicament { IdMedicament = 1, Name = "Bioprazol", Type = "Jakis typ bioprazolu", Description = "Jakis opis bioprazolu" });
            generatedMedicamentData.Add(new Medicament { IdMedicament = 2, Name = "Ibuprofen", Type = "Jakis typ Iburpofenu", Description = "Jakis opis Ibuprofenu" });
            return generatedMedicamentData;
        }

        private List<Doctor> seedDoctorData()
        {
            var generatedDoctorData = new List<Doctor>();
            generatedDoctorData.Add(new Doctor { IdDoctor = 1, FirstName = "Grzegorz", LastName = "Migawa", Email = "GregMig@promed.com" });
            generatedDoctorData.Add(new Doctor { IdDoctor = 2, FirstName = "Katarzyna", LastName = "Koza", Email = "KatKoz@promed.com" });
            return generatedDoctorData;
        }

        private List<Prescription> seedPrescriptionData()
        {
            var generatedPrescriptionData = new List<Prescription>();
            generatedPrescriptionData.Add(new Prescription { IdPrescription = 1, Date = DateTime.Parse("2020-06-19"), DueDate = DateTime.Parse("2020-06-19").AddDays(3), IdDoctor = 1, IdPatient = 1});
            generatedPrescriptionData.Add(new Prescription { IdPrescription = 2, Date = DateTime.Parse("2020-06-18"), DueDate = DateTime.Parse("2020-06-17").AddDays(7), IdDoctor = 1, IdPatient = 2});
            generatedPrescriptionData.Add(new Prescription { IdPrescription = 3, Date = DateTime.Parse("2020-06-17"), DueDate = DateTime.Parse("2020-06-18").AddDays(11), IdDoctor = 2, IdPatient = 1});
            generatedPrescriptionData.Add(new Prescription { IdPrescription = 4, Date = DateTime.Parse("2020-06-20"), DueDate = DateTime.Parse("2020-06-20").AddDays(6), IdDoctor = 2, IdPatient = 2});
            return generatedPrescriptionData;
        }

        private List<Prescription_Medicament> seedPreMedData()
        {
            var generatedPreMedData = new List<Prescription_Medicament>();
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 1, IdMedicament = 1, Dose = 100, Details = "Detale relacji PreMed1"});
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 1, IdMedicament = 2, Dose = 50, Details = "Detale relacji PreMed2"});
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 2, IdMedicament = 1, Dose = 133, Details = "Detale relacji PreMed3"});
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 3, IdMedicament = 2, Dose = 122, Details = "Detale relacji PreMed4"});
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 4, IdMedicament = 1, Dose = 323, Details = "Detale relacji PreMed5"});
            generatedPreMedData.Add(new Prescription_Medicament { IdPrescription = 4, IdMedicament = 2, Dose = 644, Details = "Detale relacji PreMed6"});
            return generatedPreMedData;
        }


    }
}
