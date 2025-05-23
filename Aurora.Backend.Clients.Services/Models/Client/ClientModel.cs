namespace Aurora.Backend.Clients.Services.Models.Client;

/// <summary>
/// Stores information about the main clients of the system
/// </summary>
public class ClientCreateModel
{
    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Location { get; set; }

    public string? CompanyType { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class ClientUpdateModel : ClientCreateModel
{
    public Guid Id { get; set; }
}
