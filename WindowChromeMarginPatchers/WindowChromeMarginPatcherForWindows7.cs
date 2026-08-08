using System.Windows;
using System.Windows.Shell;

namespace WindowChromeMarginFixup
{
    public class WindowChromeMarginPatcherForWindows7 : WindowChromeMarginPatcher
    {
        public WindowChromeMarginPatcherForWindows7(Window window) : base(window)
        {
        }

        protected override Thickness DetermineRootMargin(Window window)
        {
            WindowChrome? windowChrome = WindowChrome.GetWindowChrome(window);
            if (windowChrome is null)
                return new Thickness(0);

            switch (window.WindowState)
            {
                case WindowState.Normal:
                    return new Thickness(0);

                case WindowState.Maximized:
                    double x =
                        SystemParameters.BorderWidth +
                        SystemParameters.FixedFrameVerticalBorderWidth +
                        SystemParameters.ThinVerticalBorderWidth;

                    double y =
                        SystemParameters.BorderWidth +
                        SystemParameters.FixedFrameHorizontalBorderHeight +
                        SystemParameters.ThinHorizontalBorderHeight;

                    Thickness margin = new(x, y, x, y);
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                    {
                        margin.Left += SystemParameters.ResizeFrameVerticalBorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                    {
                        margin.Right += SystemParameters.ResizeFrameVerticalBorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                    {
                        margin.Bottom += SystemParameters.ResizeFrameHorizontalBorderHeight;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                    {
                        margin.Top += SystemParameters.ResizeFrameHorizontalBorderHeight;
                    }
                    return margin;

                default:
                    return new Thickness(0);
            }
        }
    }
}
