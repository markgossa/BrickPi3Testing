using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using BrickPi3.Sensors;
using BrickPi3.Models;
using System.Diagnostics;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private async Task TestNXTTouchSensor()
        {
            NXTTouchSensor touch = new NXTTouchSensor(brick, BrickPortSensor.PORT_S1);
            int count = 0;
            while (count < 400)
            {
                Debug.WriteLine(string.Format($"NXT Touch sensor: {touch.IsPressed()}"));
                await Task.Delay(100);
                count++;
            }
        }
    }
}
