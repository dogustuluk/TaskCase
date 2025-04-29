using TaskCase.Application.Common.GenericObjects;

namespace TaskCase.Application.Common.Extensions;

public static class ExceptionHandler
{
    public static void Handle(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Log(ex.Message);
            throw;
        }
    }

    public static async Task HandleAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            Log(ex.Message);
            throw;
        }
    }

    public static async Task<T> HandleAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            Log(ex.Message);
            throw;
        }
    }

    public static async Task<OptResult<T>> HandleOptResultAsync<T>(Func<Task<OptResult<T>>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            Log(ex.Message);
            return OptResult<T>.Failure(ex.Message);
        }
    }

    private static void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}

