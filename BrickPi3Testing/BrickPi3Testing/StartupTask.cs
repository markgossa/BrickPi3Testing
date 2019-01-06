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
    public sealed class StartupTask : IBackgroundTask
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
            TestNXTUS().Wait();

            // Reset
            brick.reset_all();
        }

        private async Task TestNXTUS()
        {
            NXTUltraSonicSensor ultra = new NXTUltraSonicSensor(brick, BrickPortSensor.PORT_S4, UltraSonicMode.Centimeter, 50);
            int count = 0;
            while (count < 400)
            {
                Debug.WriteLine(string.Format($"NXT US, Distance: {ultra.ReadDistance()}"));
                await Task.Delay(100);
                count++;
            }
        }
    }
}
