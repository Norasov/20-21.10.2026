namespace Application.DTOs.Authors;

public class AuthorDTO
{
    public string FullName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int BirthYear { get; set; }
}
