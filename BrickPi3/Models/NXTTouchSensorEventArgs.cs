using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickPi3.Models
{
    public class NXTTouchSensorEventArgs : EventArgs
    {
        public bool IsPressed { get; set; }
    }
}
