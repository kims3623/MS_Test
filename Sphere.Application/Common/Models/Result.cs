namespace Sphere.Application.Common.Models;

/// <summary>
/// Generic result wrapper for operation outcomes
/// </summary>
public class Result
{
    public bool Succeeded { get; init; }
    public string[] Errors { get; init; }

    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public static Result Success() => new(true, Array.Empty<string>());

    public static Result Failure(IEnumerable<string> errors) => new(false, errors);

    public static Result Failure(string error) => new(false, new[] { error });
}

/// <summary>
/// Generic result wrapper with data
/// </summary>
public class Result<T> : Result
{
    public T? Data { get; init; }

    internal Result(bool succeeded, T? data, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, data, Array.Empty<string>());

    public static new Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors);

    public static new Result<T> Failure(string error) => new(false, default, new[] { error });
}
