using System.Runtime.InteropServices.JavaScript;
using ErrorOr;

namespace Domain.Common;

public class DomainException(Error error) : Exception(error.Description)
{
    public Error Error { get; } = error; 
}