namespace SharedKernel;

public record Error(string Code, string Message, object? Details)
{
    public static readonly Error None = new(string.Empty, string.Empty, default);
}