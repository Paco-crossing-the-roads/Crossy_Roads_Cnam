using UnityEngine;

namespace Assets.Scripts
{
    public static class PauseManager
    {
        private static bool isPaused = false;

        public static bool IsPaused
        {
            get { return isPaused; }
        }

        public static void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
}
