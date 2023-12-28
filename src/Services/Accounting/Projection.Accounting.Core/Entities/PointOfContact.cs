using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Core.Entities;

public record PointOfContact
{
    public string Name { get; init; }

    [EmailAddress]
    public string Email { get; init; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; init; }
    //public Address Address { get; init; }
    public string Notes { get; init; }
}

public record Address
{
    public string AddressLine1 { get; init; }
    public string AddressLine2 { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string CountryId { get; init; }
}
