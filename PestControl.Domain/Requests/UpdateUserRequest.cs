namespace PestControl.Domain.Requests;

public class UpdateUserRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Email { get; set; }
}
