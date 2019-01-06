using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using BrickPi3;
using BrickPi3.Models;
using BrickPi3.Sensors;
using System.Diagnostics;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        //private BackgroundTaskDeferral deferral;
        private Brick brick = new Brick();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Ensure that application doesn't end prematurely
            //deferral = taskInstance.GetDeferral();

            // Initialize the brick
            brick.InitSPI();

            // Run methods
            TestNXTUltrasonic().Wait();

            // Reset
            brick.reset_all();
        }
    }
}
