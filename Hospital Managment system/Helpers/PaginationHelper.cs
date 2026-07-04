namespace Hospital_Managment_system.Helpers;

/// <summary>
/// Helper class for pagination operations.
/// </summary>
public static class PaginationHelper
{
    /// <summary>
    /// Calculate total pages.
    /// </summary>
    public static int CalculateTotalPages(int totalItems, int pageSize)
    {
        return (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    /// <summary>
    /// Get skip count for pagination.
    /// </summary>
    public static int GetSkipCount(int pageNumber, int pageSize)
    {
        return (pageNumber - 1) * pageSize;
    }

    /// <summary>
    /// Validate page number and size.
    /// </summary>
    public static (int pageNumber, int pageSize) ValidatePageParameters(int pageNumber, int pageSize, int minPageSize = 1, int maxPageSize = 100)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < minPageSize)
            pageSize = minPageSize;
        else if (pageSize > maxPageSize)
            pageSize = maxPageSize;

        return (pageNumber, pageSize);
    }
}
