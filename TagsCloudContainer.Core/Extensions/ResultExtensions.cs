namespace TagsCloudContainer.Core.Extensions;

public static class ResultExtensions
{
    public static Result<TOutput> Then<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, TOutput> continuation)
    {
        return input.Then(inp => Result<TOutput>.Of(() => continuation(inp)));
    }

    public static Result<None> Then<TInput, TOutput>(
        this Result<TInput> input,
        Action<TInput> continuation)
    {
        return input.Then(inp => Result.OfAction(() => continuation(inp)));
    }

    public static Result<None> Then<TInput>(
        this Result<TInput> input,
        Action<TInput> continuation)
    {
        return input.Then(inp => Result.OfAction(() => continuation(inp)));
    }

    public static Result<TOutput> Then<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, Result<TOutput>> continuation)
    {
        return input.IsSuccess
            ? continuation(input.Value)
            : Result<TOutput>.Fail(input.Error!);
    }

    public static Result<TInput> OnFail<TInput>(
        this Result<TInput> input,
        Action<Error> handleError)
    {
        if (input.IsFail)
        {
            handleError(input.Error!);
        }

        return input;
    }

    public static Result<TInput> RefineErrorMessage<TInput>(
        this Result<TInput> input,
        string errorMessage)
    {
        return input.ReplaceErrorMessage(err => errorMessage + ". " + err);
    }

    public static Result<TInput> ReplaceErrorMessage<TInput>(
        this Result<TInput> input,
        Func<string, string> replaceError)
    {
        if (input.IsSuccess)
        {
            return input;
        }

        var newError = new Error(replaceError(input.Error!.Message));
        return input.ReplaceError(_ => newError);
    }

    public static Result<TInput> ReplaceError<TInput>(
        this Result<TInput> input,
        Func<Error, Error> replaceError)
    {
        return input.IsSuccess
            ? input
            : Result<TInput>.Fail(replaceError(input.Error!));
    }
}