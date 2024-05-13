using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
