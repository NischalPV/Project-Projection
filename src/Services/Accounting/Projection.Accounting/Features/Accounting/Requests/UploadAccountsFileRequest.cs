namespace Projection.Accounting;

public record class UploadAccountsFileRequest
{
    public IFormFile AccountsFile { get; set; }

}
