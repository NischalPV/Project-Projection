namespace Projection.Accounting.Features.Accounting.Requests;

public record CreateAccountRequest
{
    public string AccountNumber { get; set; } = AccountNumberGenerator(10);
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public double Balance { get; set; }
    public string CurrencyId { get; set; }
    public string Description { get; set; }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }

    private static Random random = new Random();

    private static string AccountNumberGenerator(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
