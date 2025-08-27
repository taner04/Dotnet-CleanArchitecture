namespace SharedKernel.Response;

public sealed class ResultT<TValue> : Result
{
    private readonly TValue? _value;

    private ResultT(TValue value) : base()
    {
        _value = value;
    }

    private ResultT(Error error) : base(error)
    {
        _value = default;
    }

    public TValue Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");

    public static implicit operator ResultT<TValue>(Error error)
    {
        return new ResultT<TValue>(error);
    }

    public static implicit operator ResultT<TValue>(TValue value)
    {
        return new ResultT<TValue>(value);
    }

    public static ResultT<TValue> Success(TValue value)
    {
        return new ResultT<TValue>(value);
    }

    public new static ResultT<TValue> Failure(Error error)
    {
        return new ResultT<TValue>(error);
    }
}