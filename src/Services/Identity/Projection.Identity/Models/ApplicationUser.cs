namespace Projection.Identity.Models;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; private set; }

    // Constructor
    public ApplicationUser() => CreatedDate = DateTime.UtcNow;

    // Constructor with CreatedDate
    public ApplicationUser(DateTime createdDate) => CreatedDate = createdDate;
}