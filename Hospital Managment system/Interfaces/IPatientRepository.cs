using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Specific repository for Patient entity.
/// </summary>
public interface IPatientRepository : IRepository<Patient>
{
    /// <summary>
    /// Get patient by user ID.
    /// </summary>
    Task<Patient?> GetByUserIdAsync(string userId);

    /// <summary>
    /// Get patient by patient number.
    /// </summary>
    Task<Patient?> GetByPatientNumberAsync(string patientNumber);

    /// <summary>
    /// Search patients by name or contact.
    /// </summary>
    Task<IEnumerable<Patient>> SearchAsync(string searchTerm);

    /// <summary>
    /// Get patient with all related data.
    /// </summary>
    Task<Patient?> GetWithDetailsAsync(int patientId);
}
