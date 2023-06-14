namespace Projection.Identity.Models.AccountViewModels;
public record ResetPasswordViewModel
{
    [Required]
    [Phone]
    public required string Phone { get; init; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; init; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; init; }

    public required string Code { get; init; }
}
