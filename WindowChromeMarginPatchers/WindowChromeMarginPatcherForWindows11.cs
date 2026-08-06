using System.Windows;
using System.Windows.Shell;

namespace SystemParametersViewer
{
    public class WindowChromeMarginPatcherForWindows11 : WindowChromeMarginPatcher
    {
        public WindowChromeMarginPatcherForWindows11(Window window) : base(window)
        {
        }

        protected override Thickness DetermineContentMargin(Window window)
        {
            Thickness margin;
            WindowChrome windowChrome;
            switch (window.WindowState)
            {
                case WindowState.Normal:
                    windowChrome = WindowChrome.GetWindowChrome(window);
                    if (windowChrome is null)
                        break;

                    margin = new Thickness(0, 0, 0, 0);
                    if (Environment.OSVersion.IsWindows11())
                    {
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                        {
                            margin.Left += SystemParameters.BorderWidth;
                        }
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                        {
                            margin.Right += SystemParameters.BorderWidth;
                        }
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                        {
                            margin.Bottom += SystemParameters.BorderWidth;
                        }
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                        {
                            margin.Top += SystemParameters.BorderWidth;
                        }
                        else
                        {
                            margin.Top -= SystemParameters.ThinHorizontalBorderHeight;
                        }
                    }
                    break;

                case WindowState.Maximized:
                    windowChrome = WindowChrome.GetWindowChrome(window);
                    if (windowChrome is null)
                        break;

                    margin = new Thickness(0, 0, 0, 0);
                    double x =
                        SystemParameters.BorderWidth +
                        SystemParameters.FixedFrameVerticalBorderWidth +
                        SystemParameters.ResizeFrameVerticalBorderWidth;
                    double y =
                        SystemParameters.BorderWidth +
                        SystemParameters.FixedFrameHorizontalBorderHeight +
                        SystemParameters.ResizeFrameHorizontalBorderHeight;
                    margin.Left += x;
                    margin.Right += x;
                    margin.Top += y;
                    margin.Bottom += y;
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                    {
                        margin.Left -= SystemParameters.BorderWidth;
                    }
                    else
                    {
                        margin.Left += SystemParameters.BorderWidth;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                    {
                        margin.Right -= SystemParameters.BorderWidth;
                    }
                    else
                    {
                        margin.Right += SystemParameters.BorderWidth;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                    {
                        margin.Bottom -= SystemParameters.BorderWidth;
                    }
                    else
                    {
                        margin.Bottom += SystemParameters.BorderWidth;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                    {
                        margin.Top -= SystemParameters.BorderWidth;
                    }
                    else
                    {
                        margin.Top += SystemParameters.BorderWidth;
                    }
                    break;

                default:
                    break;
            }
            return margin;
        }
    }
}
