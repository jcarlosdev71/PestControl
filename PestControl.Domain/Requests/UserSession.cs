namespace PestControl.Domain.Requests;

public record UserSession(string? Id, string? Name, string? Email, string? Role);
