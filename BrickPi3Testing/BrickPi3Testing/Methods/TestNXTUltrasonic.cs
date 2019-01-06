using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using BrickPi3.Sensors;
using BrickPi3.Models;
using System.Diagnostics;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private async Task TestNXTUltrasonic()
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
