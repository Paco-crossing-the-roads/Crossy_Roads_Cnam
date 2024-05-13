namespace Assets.Scripts
{
    public static class PauseManager
    {
        public static bool isPaused = false;

        public static void TogglePause()
        {
            isPaused = !isPaused;
        }
    }

}
