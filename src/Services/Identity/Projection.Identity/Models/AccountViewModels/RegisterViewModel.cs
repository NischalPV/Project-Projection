namespace Projection.Identity.Models.AccountViewModels;
public record RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public required string Email { get; init; }

    [Required]
    [Phone]
    [Display(Name ="Phone")]
    public required string Phone { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; init; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; init; }

    public required ApplicationUser User { get; init; }
}