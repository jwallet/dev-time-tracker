enum MenuEnum
{
    Pause,
    Resume,
    Reset,
    Settings,
    Exit
}

internal static class Menu
{
    internal static string ToKey(this MenuEnum menu)
    {
        return menu.ToString().ToLowerInvariant().Replace(" ", string.Empty);
    }
}