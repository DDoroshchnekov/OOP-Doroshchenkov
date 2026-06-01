public static class NullableHelper
{
    public static string GetValue(string? value)
    {
        return value ?? "Default";
    }
}