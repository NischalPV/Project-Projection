namespace Projection.UI.Web.Models;

public record Account(string AccountNumber,
                      string GSTNumber,
                      string PANNumber,
                      double Balance,
                      string Currency,
                      DateTime CreatedDate,
                      string CreatedBy,
                      DateTime? ModifiedDate,
                      string ModifiedBy,
                      string Status
    );
