using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    public class GameTimer
    {
        public System.Windows.Threading.DispatcherTimer Timer { get; set; }

        public GameTimer()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();

            Timer.Interval = TimeSpan.FromMilliseconds(1);
        }

    }
}
