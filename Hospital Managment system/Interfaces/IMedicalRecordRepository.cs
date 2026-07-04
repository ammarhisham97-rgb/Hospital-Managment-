using Hospital_Managment_system.Models;

namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Specific repository for MedicalRecord entity.
/// </summary>
public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    /// <summary>
    /// Get medical records for a patient.
    /// </summary>
    Task<IEnumerable<MedicalRecord>> GetPatientRecordsAsync(int patientId);

    /// <summary>
    /// Get medical records created by a doctor.
    /// </summary>
    Task<IEnumerable<MedicalRecord>> GetDoctorRecordsAsync(int doctorId);

    /// <summary>
    /// Get medical record by appointment ID.
    /// </summary>
    Task<MedicalRecord?> GetByAppointmentAsync(int appointmentId);

    /// <summary>
    /// Get medical record with prescriptions.
    /// </summary>
    Task<MedicalRecord?> GetWithPrescriptionsAsync(int recordId);
}
