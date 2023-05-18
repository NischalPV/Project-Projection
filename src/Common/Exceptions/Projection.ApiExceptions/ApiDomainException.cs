namespace Projection.Common.ApiExceptions;

public class ApiDomainException : Exception
{
    public ApiDomainException() { }
    public ApiDomainException(string message) : base(message) { }
    public ApiDomainException(string message, Exception innerException) : base(message, innerException) { }
}
