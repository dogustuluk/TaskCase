namespace TaskCase.Application.Common.GenericObjects;

public class OptResultClient
{
    public string Message { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
    public bool Succeeded { get; set; }
    public string Data { get; set; }
    public Exception Exception { get; set; }
    public int Code { get; set; }

}
public class OptResult<T> : IOptResult<T>
{
    public string Message { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
    public bool Succeeded { get; set; }
    public T Data { get; set; }
    public Exception Exception { get; set; }
    public int Code { get; set; }

    #region  Sync Methods 

    #region Success 

    public static OptResult<T> Success()
    {
        return new OptResult<T>
        {
            Succeeded = true
        };
    }

    public static OptResult<T> Success(string message)
    {
        return new OptResult<T>
        {
            Succeeded = true,
            Messages = new List<string> { message }
        };
    }

    public static OptResult<T> Success(T data)
    {
        return new OptResult<T>
        {
            Succeeded = true,
            Data = data
        };
    }

    public static OptResult<T> Success(T data, string message)
    {
        return new OptResult<T>
        {
            Succeeded = true,
            Messages = new List<string> { message },
            Data = data
        };
    }

    #endregion

    #region Fail 

    public static OptResult<T> Failure()
    {
        return new OptResult<T>
        {
            Succeeded = false
        };
    }

    public static OptResult<T> Failure(string message)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Messages = new List<string> { message }
        };
    }

    public static OptResult<T> Failure(List<string> messages)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Messages = messages
        };
    }

    public static OptResult<T> Failure(T data)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Data = data
        };
    }

    public static OptResult<T> Failure(T data, string message)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Messages = new List<string> { message },
            Data = data
        };
    }

    public static OptResult<T> Failure(T data, List<string> messages)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Messages = messages,
            Data = data
        };
    }

    public static OptResult<T> Failure(Exception exception)
    {
        return new OptResult<T>
        {
            Succeeded = false,
            Exception = exception
        };
    }
    #endregion

    #endregion

    #region Async 

    #region Success 

    public static Task<OptResult<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<OptResult<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<OptResult<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    #endregion

    #region Fail

    public static Task<OptResult<T>> FailureAsync()
    {
        return Task.FromResult(Failure());
    }

    public static Task<OptResult<T>> FailureAsync(string message)
    {
        return Task.FromResult(Failure(message));
    }

    public static Task<OptResult<T>> FailureAsync(List<string> messages)
    {
        return Task.FromResult(Failure(messages));
    }

    public static Task<OptResult<T>> FailureAsync(T data)
    {
        return Task.FromResult(Failure(data));
    }

    public static Task<OptResult<T>> FailureAsync(T data, string message)
    {
        return Task.FromResult(Failure(data, message));
    }

    public static Task<OptResult<T>> FailureAsync(T data, List<string> messages)
    {
        return Task.FromResult(Failure(data, messages));
    }

    public static Task<OptResult<T>> FailureAsync(Exception exception)
    {
        return Task.FromResult(Failure(exception));
    }

    #endregion

    #endregion
}
