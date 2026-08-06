namespace SystemParametersViewer;

internal static class OperatingSystemWindowsExtensions
{
    public static bool IsWindows11(this OperatingSystem os)
    {
        return os.Platform == PlatformID.Win32NT
            && os.Version.Major == 10
            && os.Version.Build >= 22000;
    }

    public static bool IsWindows10(this OperatingSystem os)
    {
        return os.Platform == PlatformID.Win32NT
            && os.Version.Major == 10
            && os.Version.Build < 22000;
    }

    public static bool IsWindows8_1(this OperatingSystem os)
    {
        return os.Platform != PlatformID.Win32NT
            && os.Version.Major == 6
            && os.Version.Minor == 3;
    }

    public static bool IsWindows8(this OperatingSystem os)
    {
        return os.Platform != PlatformID.Win32NT
            && os.Version.Major == 6
            && os.Version.Minor == 2;
    }

    public static bool IsWindows7(this OperatingSystem os)
    {
        return os.Platform != PlatformID.Win32NT
            && os.Version.Major == 6
            && os.Version.Minor == 1;
    }

    public static float WindowsVersion(this OperatingSystem os)
    {
        if (os.IsWindows11())
            return 11.0f;

        if (os.IsWindows10())
            return 10.0f;

        if (os.IsWindows8_1())
            return 8.1f;

        if (os.IsWindows8())
            return 8.0f;

        if (os.IsWindows7())
            return 7.0f;

        return float.NaN;
    }
}
