namespace TagsCloudContainer.Core;

public readonly record struct Result<T>
{
    public Error? Error { get; } = null;
    internal T Value { get; }
    public bool IsSuccess => Error is null;
    public bool IsFail => !IsSuccess;

    private Result(Error? error = null, T value = default(T))
    {
        Error = error;
        Value = value;
    }

    public static Result<T> Ok(T value) => new(null, value);

    public static Result<T> Fail(Error error) => new(error);

    public T GetValueOrThrow()
    {
        if (IsSuccess)
        {
            return Value;
        }

        throw new InvalidOperationException($"No value. Only Error {Error!.Message}");
    }

    public static Result<T> Of(Func<T> f, Error? error = null)
    {
        try
        {
            return Ok(f());
        }
        catch (Exception e)
        {
            return Fail(error ?? new Error(e.Message));
        }
    }

    public static implicit operator Result<T>(T value)
    {
        return Ok(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Fail(error);
    }
}