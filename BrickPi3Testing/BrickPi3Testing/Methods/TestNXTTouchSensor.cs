using BrickPi3.Models;
using BrickPi3.Sensors;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private async Task TestNXTTouchSensor()
        {
            var touch = new NXTTouchSensor(brick, BrickPortSensor.PORT_S2);

            while (true)
            {
                Debug.WriteLine(string.Format($"NXT Touch sensor: {touch.IsPressed()}"));
                await Task.Delay(100);
            }
        }
    }
}
