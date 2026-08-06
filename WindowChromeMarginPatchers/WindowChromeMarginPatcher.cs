using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace SystemParametersViewer;

public abstract partial class WindowChromeMarginPatcher
{
    public WindowChromeMarginPatcher(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);
        _window = window;
    }

    private readonly Window _window;

    private FrameworkElement? WindowRootFrameworkElement
    {
        get
        {
            if (VisualTreeHelper.GetChildrenCount(_window) > 0 && VisualTreeHelper.GetChild(_window, 0) is FrameworkElement frameworkElement)
                return frameworkElement;

            return null;
        }
    }

    public void Enable()
    {
        _window.StateChanged += OnWindowStateChanged;
        if (_window.IsLoaded)
        {
            DetermineContentMargin(_window);
            return;
        }
        _window.Loaded += OnWindowLoaded;
        void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _window.Loaded -= OnWindowLoaded;
            WindowRootFrameworkElement?.Margin = DetermineContentMargin(_window);
        }
    }

    public void Disable()
    {
        _window.StateChanged -= OnWindowStateChanged;
    }

    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        WindowRootFrameworkElement?.Margin = DetermineContentMargin(_window);
    }

    protected abstract Thickness DetermineContentMargin(Window window);
}
