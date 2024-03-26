using System.ComponentModel.DataAnnotations.Schema;
using Projection.Common.BaseEntities;

namespace Projection.Accounting.Core.Entities;

public record AccountTransaction : BaseEntity<string>
{
    [ForeignKey(nameof(Account))]
    public string AccountId { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }

    [ForeignKey(nameof(TransactionType))]
    public int TransactionTypeId { get; set; }


    public Account Account { get; set; }
    public TransactionType TransactionType { get; set; }

}

public record TransactionType : BaseEntity<int>
{
    public int Multiplier { get; set; }

    public TransactionType()
    {
    }

    public TransactionType(int id)
    {
        Id = id;
    }
}
