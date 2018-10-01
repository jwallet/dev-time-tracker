namespace DevTimeTracker
{
    enum MenuEnum
    {
        Pause,
        Resume,
        Reset,
        Settings,
        Exit
    }

    static class Menu
    {
        public static string ToKey(this MenuEnum menu)
        {
            return menu.ToString().ToLowerInvariant().Replace(" ", string.Empty);
        }
    }
}
