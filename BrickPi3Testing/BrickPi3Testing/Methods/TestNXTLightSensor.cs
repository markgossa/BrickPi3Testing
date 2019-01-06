using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using BrickPi3.Sensors;
using BrickPi3.Models;
using System.Diagnostics;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private async Task TestNXTLightSensor()
        {
            NXTLightSensor light = new NXTLightSensor(brick, BrickPortSensor.PORT_S3, LightMode.Ambient, 50);
            int count = 0;
            while (count < 400)
            {
                Debug.WriteLine(string.Format($"NXT Light value: {light.Value}"));
                await Task.Delay(100);
                count++;
            }
        }
    }
}
