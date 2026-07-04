namespace Hospital_Managment_system.Models;

/// <summary>
/// Represents the schedule of a doctor for availability management.
/// </summary>
public class DoctorSchedule : BaseEntity
{
    /// <summary>
    /// Doctor ID.
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Day of the week (0=Sunday, 6=Saturday).
    /// </summary>
    public int DayOfWeek { get; set; }

    /// <summary>
    /// Start time for availability.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// End time for availability.
    /// </summary>
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Appointment duration in minutes.
    /// </summary>
    public int AppointmentDurationInMinutes { get; set; } = 30;

    /// <summary>
    /// Flag indicating if this schedule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public Doctor? Doctor { get; set; }
}
