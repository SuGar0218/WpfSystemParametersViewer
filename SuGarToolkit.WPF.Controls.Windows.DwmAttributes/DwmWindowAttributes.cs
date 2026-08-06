using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace SuGarToolkit.WPF.Controls.Windows;

public class DwmWindowAttributes
{
    public static Color? GetBorderColor(Window target) => (Color?)target.GetValue(BorderColorProperty);
    public static void SetBorderColor(Window target, Color? value) => target.SetValue(BorderColorProperty, value);

    public static readonly DependencyProperty BorderColorProperty = DependencyProperty.RegisterAttached(
        "BorderColor",
        typeof(Color?),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(Color?), OnBorderColorChanged)
    );

    private static void OnBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        Color? color = (Color?)e.NewValue;
        nint handle = RetrieveWindowHandle(window);
        if (handle != nint.Zero)
        {
            Win32DwmWindowAttributes.SetBorderColor(RetrieveWindowHandle(window), color);
            return;
        }
        ExecuteOnceAfterSourceInitialized(window, () =>
        {
            Win32DwmWindowAttributes.SetBorderColor(RetrieveWindowHandle(window), color);
        });
    }

    public static Color? GetTitleBarColor(Window target) => (Color?)target.GetValue(TitleBarColorProperty);
    public static void SetTitleBarColor(Window target, Color? value) => target.SetValue(TitleBarColorProperty, value);

    public static readonly DependencyProperty TitleBarColorProperty = DependencyProperty.RegisterAttached(
        "TitleBarColor",
        typeof(Color?),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(Color?), OnTitleBarColorChanged)
    );

    private static void OnTitleBarColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        Color? color = (Color?)e.NewValue;
        nint handle = RetrieveWindowHandle(window);
        if (handle != nint.Zero)
        {
            Win32DwmWindowAttributes.SetTitleBarColor(RetrieveWindowHandle(window), color);
            return;
        }
        ExecuteOnceAfterSourceInitialized(window, () =>
        {
            Win32DwmWindowAttributes.SetTitleBarColor(RetrieveWindowHandle(window), color);
        });
    }

    public static Color? GetTitleTextColor(Window target) => (Color?)target.GetValue(TitleTextColorProperty);
    public static void SetTitleTextColor(Window target, Color? value) => target.SetValue(TitleTextColorProperty, value);

    public static readonly DependencyProperty TitleTextColorProperty = DependencyProperty.RegisterAttached(
        "TitleTextColor",
        typeof(Color?),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(Color?), OnTitleTextColorChanged)
    );

    private static void OnTitleTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        Color? color = (Color?)e.NewValue;
        nint handle = RetrieveWindowHandle(window);
        if (handle != nint.Zero)
        {
            Win32DwmWindowAttributes.SetTitleTextColor(RetrieveWindowHandle(window), color);
            return;
        }
        ExecuteOnceAfterSourceInitialized(window, () =>
        {
            Win32DwmWindowAttributes.SetTitleTextColor(RetrieveWindowHandle(window), color);
        });
    }

    public static bool GetIsDarkMode(Window target) => (bool)target.GetValue(IsDarkModeProperty);
    public static void SetIsDarkMode(Window target, bool value) => target.SetValue(IsDarkModeProperty, value);

    public static readonly DependencyProperty IsDarkModeProperty = DependencyProperty.RegisterAttached(
        "IsDarkMode",
        typeof(bool),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(bool), OnIsDarkModeChanged)
    );

    private static void OnIsDarkModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        bool enable = (bool)e.NewValue;
        nint handle = RetrieveWindowHandle(window);
        if (handle != nint.Zero)
        {
            Win32DwmWindowAttributes.SetIsDarkMode(RetrieveWindowHandle(window), enable);
            return;
        }
        ExecuteOnceAfterSourceInitialized(window, () =>
        {
            Win32DwmWindowAttributes.SetIsDarkMode(RetrieveWindowHandle(window), enable);
        });
    }

    public static WindowCornerRoundness GetCornerRoundness(Window target) => (WindowCornerRoundness)target.GetValue(CornerRoundnessProperty);
    public static void SetCornerRoundness(Window target, WindowCornerRoundness value) => target.SetValue(CornerRoundnessProperty, value);

    public static readonly DependencyProperty CornerRoundnessProperty = DependencyProperty.RegisterAttached(
        "CornerRoundness",
        typeof(WindowCornerRoundness),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(WindowCornerRoundness), OnCornerRoundnessChanged)

    );

    private static void OnCornerRoundnessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        WindowCornerRoundness cornerRoundness = (WindowCornerRoundness)e.NewValue;
        Win32DwmWindowAttributes.SetCornerRoundness(RetrieveWindowHandle(window), cornerRoundness);
    }

    public static WindowSystemBackdrop GetSystemBackdrop(Window target) => (WindowSystemBackdrop)target.GetValue(SystemBackdropProperty);
    public static void SetSystemBackdrop(Window target, WindowSystemBackdrop value) => target.SetValue(SystemBackdropProperty, value);

    public static readonly DependencyProperty SystemBackdropProperty = DependencyProperty.RegisterAttached(
        "SystemBackdrop",
        typeof(WindowSystemBackdrop),
        typeof(DwmWindowAttributes),
        new PropertyMetadata(default(WindowSystemBackdrop), OnSystemBackdropChanged)
    );

    private static void OnSystemBackdropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Window window = (Window)d;
        WindowSystemBackdrop systemBackdrop = (WindowSystemBackdrop)e.NewValue;
        nint handle = RetrieveWindowHandle(window);
        if (handle != nint.Zero)
        {
            Win32DwmWindowAttributes.SetSystemBackdrop(RetrieveWindowHandle(window), systemBackdrop);
            return;
        }
        ExecuteOnceAfterSourceInitialized(window, () =>
        {
            Win32DwmWindowAttributes.SetSystemBackdrop(RetrieveWindowHandle(window), systemBackdrop);
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static nint RetrieveWindowHandle(Window window) => new WindowInteropHelper(window).Handle;

    private static void ExecuteOnceAfterSourceInitialized(Window window, Action action)
    {
        window.SourceInitialized += ExecuteActionOnSourceInitialized;
        void ExecuteActionOnSourceInitialized(object? sender, EventArgs e)
        {
            window.SourceInitialized -= ExecuteActionOnSourceInitialized;
            action.Invoke();
        }
    }
}
