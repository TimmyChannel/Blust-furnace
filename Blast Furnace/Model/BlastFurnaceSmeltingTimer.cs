using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ViewLab10.Model
{
    public class BlastFurnaceSmeltingTimer : IObserver
    {
        private BlastFurnace _observable_furnace;
        private byte _furnace_state;
        private byte _prev_state;
        private Stopwatch _furnace_stopwatch;

        public BlastFurnaceSmeltingTimer(BlastFurnace observable_furnace)
        {
            _observable_furnace = observable_furnace;
            observable_furnace.Attach(this);
            _furnace_stopwatch = new Stopwatch();
        }

        public void Update(object obj)
        {
            _prev_state = _furnace_state;
            _furnace_state = (byte)_observable_furnace._current_state;
            if (_furnace_state != (int)FurnaceStates.Offline)
            {
                if (_prev_state == (int)FurnaceStates.Offline)
                {
                    _prev_state = _furnace_state;
                    _furnace_stopwatch.Restart();
                    _furnace_stopwatch.Start();
                }
            }
            else
            {
                _furnace_stopwatch.Stop();
            }
        }

        public string GetWorkTime()
        {
            TimeSpan ts = _furnace_stopwatch.Elapsed;
            return String.Format("{0:00}:{1:00}", 
                ts.Minutes, ts.Seconds);
        }
    }
}
