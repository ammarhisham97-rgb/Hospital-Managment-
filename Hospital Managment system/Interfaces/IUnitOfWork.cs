using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Unit of Work pattern interface to coordinate multiple repositories.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Patient repository.
    /// </summary>
    IPatientRepository Patients { get; }

    /// <summary>
    /// Doctor repository.
    /// </summary>
    IDoctorRepository Doctors { get; }

    /// <summary>
    /// Appointment repository.
    /// </summary>
    IAppointmentRepository Appointments { get; }

    /// <summary>
    /// Medical record repository.
    /// </summary>
    IMedicalRecordRepository MedicalRecords { get; }

    /// <summary>
    /// Department repository.
    /// </summary>
    IDepartmentRepository Departments { get; }

    /// <summary>
    /// Generic repository for any entity.
    /// </summary>
    IRepository<T> Repository<T>() where T : class;

    /// <summary>
    /// Save all changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Begin a transaction.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commit the current transaction.
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Rollback the current transaction.
    /// </summary>
    Task RollbackAsync();
}
