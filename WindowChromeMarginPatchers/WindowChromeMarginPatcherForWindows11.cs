using System.Windows;
using System.Windows.Shell;

namespace WindowChromeMarginFixup
{
    public class WindowChromeMarginPatcherForWindows11 : WindowChromeMarginPatcher
    {
        public WindowChromeMarginPatcherForWindows11(Window window) : base(window)
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
                    Thickness normalMargin = new(0, 0, 0, 0);
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                    {
                        normalMargin.Left += SystemParameters.BorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                    {
                        normalMargin.Right += SystemParameters.BorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                    {
                        normalMargin.Bottom += SystemParameters.BorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                    {
                        normalMargin.Top += SystemParameters.BorderWidth;
                    }
                    return normalMargin;

                case WindowState.Maximized:
                    double x =
                        SystemParameters.BorderWidth +
                        SystemParameters.ResizeFrameVerticalBorderWidth +
                        SystemParameters.ThinVerticalBorderWidth;

                    double y =
                        SystemParameters.BorderWidth +
                        SystemParameters.ResizeFrameHorizontalBorderHeight +
                        SystemParameters.ThinHorizontalBorderHeight;

                    Thickness maximizedMargin = new(x, y, x, y);
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Left))
                    {
                        maximizedMargin.Left +=
                            SystemParameters.FixedFrameVerticalBorderWidth +
                            SystemParameters.ThinVerticalBorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Right))
                    {
                        maximizedMargin.Right +=
                            SystemParameters.FixedFrameVerticalBorderWidth +
                            SystemParameters.ThinVerticalBorderWidth;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Bottom))
                    {
                        maximizedMargin.Bottom +=
                            SystemParameters.FixedFrameHorizontalBorderHeight +
                            SystemParameters.ThinHorizontalBorderHeight;
                    }
                    if (!windowChrome.NonClientFrameEdges.HasFlag(NonClientFrameEdges.Top))
                    {
                        maximizedMargin.Top +=
                            SystemParameters.FixedFrameHorizontalBorderHeight +
                            SystemParameters.ThinHorizontalBorderHeight;
                    }
                    return maximizedMargin;

                default:
                    return new Thickness(0);
            }
        }
    }
}
