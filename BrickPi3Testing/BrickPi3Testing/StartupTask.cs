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
        private Brick brick = new Brick();
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Initialize the brick
            brick.InitSPI();

            // Run methods
            TestNXTUS();

            // Reset
            brick.reset_all();
        }

        private async Task TestNXTUS()
        {
            NXTUltraSonicSensor ultra = new NXTUltraSonicSensor(brick, BrickPortSensor.PORT_S4);
            for (int i = 0; i < ultra.NumberOfModes(); i++)
            {
                int count = 0;
                while (count < 50)
                {
                    Debug.WriteLine(string.Format("NXT US, Distance: {0}, ReadAsString: {1}, Selected mode: {2}",
                        ultra.ReadDistance(), ultra.ReadAsString(), ultra.SelectedMode()));
                    await Task.Delay(2000);
                    count++;
                }
                ultra.SelectNextMode();
            }
        }
    }
}
