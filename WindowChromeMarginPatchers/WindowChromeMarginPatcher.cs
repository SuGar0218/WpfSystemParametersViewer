using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace WindowChromeMarginFixup;

public abstract partial class WindowChromeMarginPatcher
{
    public WindowChromeMarginPatcher(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);
        _window = window;
    }

    private readonly Window _window;
    private static readonly DependencyPropertyDescriptor _windowChromeDescriptor = DependencyPropertyDescriptor.FromProperty(WindowChrome.WindowChromeProperty, typeof(Window));

    public bool IsEnabled
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;
            if (IsEnabled)
            {
                _window.StateChanged += OnWindowStateChanged;
                _windowChromeDescriptor.AddValueChanged(_window, OnWindowChromeChanged);
                if (_window.IsLoaded)
                {
                    DetermineRootMargin(_window);
                    return;
                }
                _window.Loaded += OnWindowLoaded;
                void OnWindowLoaded(object sender, RoutedEventArgs e)
                {
                    _window.Loaded -= OnWindowLoaded;
                    FixupMargin();
                }
            }
            else
            {
                _window.StateChanged -= OnWindowStateChanged;
                _windowChromeDescriptor.RemoveValueChanged(this, OnWindowChromeChanged);
            }
        }
    }
    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        FixupMargin();
    }

    private void OnWindowChromeChanged(object? sender, EventArgs e)
    {
        FixupMargin();
    }

    private void FixupMargin()
    {
        if (VisualTreeHelper.GetChildrenCount(_window) <= 0)
            return;

        if (VisualTreeHelper.GetChild(_window, 0) is not FrameworkElement root)
            return;

        root.Margin = DetermineRootMargin(_window);
    }

    protected abstract Thickness DetermineRootMargin(Window window);
}
