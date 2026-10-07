namespace Chicago311Api.Models;

// Represents one row in the service_requests MySQL table.
public class ServiceRequest
{
    // Chicago's unique service request number.
    public string SrNumber { get; set; } = "";

    // Type of service request.
    public string SrType { get; set; } = "";

    // Short code for the request type.
    public string? SrShortCode { get; set; }

    // Department responsible for the request.
    public string? OwnerDepartment { get; set; }

    // Current status of the request.
    public string Status { get; set; } = "";

    // How the request was created.
    public string? Origin { get; set; }

    // When the request was created.
    public DateTime? CreatedDate { get; set; }

    // When the request was last modified.
    public DateTime? LastModifiedDate { get; set; }

    // When the request was closed.
    public DateTime? ClosedDate { get; set; }

    // Street address of the request.
    public string? StreetAddress { get; set; }

    // City of the request.
    public string? City { get; set; }

    // State of the request.
    public string? State { get; set; }

    // ZIP code of the request.
    public string? ZipCode { get; set; }

    // Chicago community area number.
    public int? CommunityArea { get; set; }

    // Chicago ward number.
    public int? Ward { get; set; }

    // Latitude of the request.
    public decimal? Latitude { get; set; }

    // Longitude of the request.
    public decimal? Longitude { get; set; }
}