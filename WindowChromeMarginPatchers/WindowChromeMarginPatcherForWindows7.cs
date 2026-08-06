using System.Windows;
using System.Windows.Shell;

namespace SystemParametersViewer
{
    public class WindowChromeMarginPatcherForWindows7 : WindowChromeMarginPatcher
    {
        public WindowChromeMarginPatcherForWindows7(Window window) : base(window)
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

                    margin = new Thickness(0);
                    if (Environment.OSVersion.IsWindows11())
                    {
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                        {
                            margin.Left -= SystemParameters.ResizeFrameVerticalBorderWidth;
                        }
                        else
                        {
                            margin.Left += SystemParameters.FixedFrameVerticalBorderWidth;
                        }
                        if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                        {
                            margin.Right -= SystemParameters.ResizeFrameVerticalBorderWidth;
                        }
                        else
                        {
                            margin.Right += SystemParameters.FixedFrameVerticalBorderWidth;
                        }
                        if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
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
                        margin.Left -= SystemParameters.ThinVerticalBorderWidth + SystemParameters.FixedFrameVerticalBorderWidth;
                    }
                    else
                    {
                        margin.Left += SystemParameters.ThinVerticalBorderWidth;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                    {
                        margin.Right -= SystemParameters.ThinVerticalBorderWidth + SystemParameters.FixedFrameVerticalBorderWidth;
                    }
                    else
                    {
                        margin.Right += SystemParameters.ThinVerticalBorderWidth;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                    {
                        margin.Bottom -= SystemParameters.ThinHorizontalBorderHeight + SystemParameters.FixedFrameHorizontalBorderHeight;
                    }
                    else
                    {
                        margin.Bottom += SystemParameters.ThinHorizontalBorderHeight;
                    }
                    if (windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                    {
                        margin.Top = 0;
                    }
                    else
                    {
                        margin.Top += SystemParameters.ThinHorizontalBorderHeight;
                    }
                    break;

                default:
                    break;
            }
            return margin;
        }
    }
}
