using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Data;

/// <summary>
/// Entity Framework Core database context for the Hospital Management System.
/// Handles all database operations and entity configurations.
/// </summary>
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Patient)
            .WithOne(p => p.User)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Doctor)
            .WithOne(d => d.User)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Department entity
        modelBuilder.Entity<Department>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Doctors)
            .WithOne(dr => dr.Department)
            .HasForeignKey(dr => dr.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure Doctor entity
        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Schedules)
            .WithOne(s => s.Doctor)
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.MedicalRecords)
            .WithOne(mr => mr.Doctor)
            .HasForeignKey(mr => mr.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Patient entity
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.MedicalRecords)
            .WithOne(mr => mr.Patient)
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure DoctorSchedule entity
        modelBuilder.Entity<DoctorSchedule>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<DoctorSchedule>()
            .HasOne(s => s.Doctor)
            .WithMany(d => d.Schedules)
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Appointment entity
        modelBuilder.Entity<Appointment>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.MedicalRecord)
            .WithOne(mr => mr.Appointment)
            .HasForeignKey<MedicalRecord>(mr => mr.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure MedicalRecord entity
        modelBuilder.Entity<MedicalRecord>()
            .HasKey(mr => mr.Id);

        modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Patient)
            .WithMany(p => p.MedicalRecords)
            .HasForeignKey(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Doctor)
            .WithMany(d => d.MedicalRecords)
            .HasForeignKey(mr => mr.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicalRecord>()
            .HasOne(mr => mr.Appointment)
            .WithOne(a => a.MedicalRecord)
            .HasForeignKey<MedicalRecord>(mr => mr.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<MedicalRecord>()
            .HasMany(mr => mr.Prescriptions)
            .WithOne(p => p.MedicalRecord)
            .HasForeignKey(p => p.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Prescription entity
        modelBuilder.Entity<Prescription>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.MedicalRecord)
            .WithMany(mr => mr.Prescriptions)
            .HasForeignKey(p => p.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // Apply query filters for soft delete
        modelBuilder.Entity<Department>()
            .HasQueryFilter(d => !d.IsDeleted);

        modelBuilder.Entity<Doctor>()
            .HasQueryFilter(d => !d.IsDeleted);

        modelBuilder.Entity<Patient>()
            .HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<Appointment>()
            .HasQueryFilter(a => !a.IsDeleted);

        modelBuilder.Entity<MedicalRecord>()
            .HasQueryFilter(mr => !mr.IsDeleted);

        modelBuilder.Entity<Prescription>()
            .HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<DoctorSchedule>()
            .HasQueryFilter(s => !s.IsDeleted);

        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted);

        // Add indexes for better query performance
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.PatientNumber)
            .IsUnique();

        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.LicenseNumber)
            .IsUnique();

        modelBuilder.Entity<Appointment>()
            .HasIndex(a => new { a.DoctorId, a.AppointmentDateTime });

        modelBuilder.Entity<MedicalRecord>()
            .HasIndex(mr => new { mr.PatientId, mr.CreatedAt });
    }
}
