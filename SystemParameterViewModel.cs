namespace SystemParametersViewer;

internal class SystemParameterViewModel
{
    public string Name { get; set; } = string.Empty;
    public Type Type { get; set; } = typeof(object);
    public object? Value { get; set; }
    public string? HelpLink { get; set; }
}
