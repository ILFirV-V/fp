namespace TagsCloudContainer.Core;

public static class Result
{
    public static Result<None> Ok()
    {
        return Result<None>.Ok(new None());
    }

    public static Result<None> Fail(Error error)
    {
        return Result<None>.Fail(error);
    }

    public static Result<None> OfAction(Action f, Error? error = null)
    {
        try
        {
            f();
            return Ok();
        }
        catch (Exception e)
        {
            return Result<None>.Fail(error ?? new Error(e.Message));
        }
    }
}