using SharedKernel.Response.Errors;

namespace SharedKernel.Response;

public interface IFailureCreatable<out TSelf>
    where TSelf : IFailureCreatable<TSelf>
{
    static abstract TSelf Failure(Error error);
}