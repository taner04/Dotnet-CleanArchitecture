namespace Application.Response
{
    public readonly record struct Error(
        string Title,
        string Message,
        int StatusCode,
        Dictionary<string, string[]>? ValidationErrors = null
    );
}
