namespace TaskCase.Application.Common.GenericObjects;

public interface IOptResult<T>
{
    string Message { get; set; }
    List<string> Messages { get; set; }
    bool Succeeded { get; set; }
    T Data { get; set; }
    Exception Exception { get; set; }
    int Code { get; set; }
}

