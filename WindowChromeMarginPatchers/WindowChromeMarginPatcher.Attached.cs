using System.Runtime.CompilerServices;
using System.Windows;

namespace SystemParametersViewer;

public partial class WindowChromeMarginPatcher
{
    public static bool GetIsEnabled(Window target) => (bool)target.GetValue(IsEnabledProperty);
    public static void SetIsEnabled(Window target, bool value) => target.SetValue(IsEnabledProperty, value);

    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
        "IsEnabled",
        typeof(bool),
        typeof(WindowChromeMarginPatcher),
        new PropertyMetadata(default(bool), OnIsEnabledChanged)
    );

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        if (e.NewValue is true)
        {
            if (!_patchers.TryGetValue(window, out WindowChromeMarginPatcher? patcher) || patcher is null)
            {
                patcher = CreateWindowChromeMarginPatcher(window);
                _patchers.Add(window, patcher);
            }
            patcher.Enable();
        }
        else if (_patchers.TryGetValue(window, out WindowChromeMarginPatcher? patcher) && patcher is not null)
        {
            patcher.Disable();
        }
    }

    private static WindowChromeMarginPatcher CreateWindowChromeMarginPatcher(Window window)
    {
        if (Environment.OSVersion.WindowsVersion() >= 10)
            return new WindowChromeMarginPatcherForWindows11(window);

        return new WindowChromeMarginPatcherForWindows7(window);
    }

    private static readonly ConditionalWeakTable<Window, WindowChromeMarginPatcher> _patchers = [];
}
