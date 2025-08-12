namespace SharedKernel.Response
{
    public readonly record struct Error(
        string Title,
        string Message,
        int StatusCode,
        IDictionary<string, string[]>? ValidationErrors = null
    );
}
