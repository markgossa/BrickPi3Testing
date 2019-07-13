using Windows.ApplicationModel.Background;
using BrickPi3;
using BrickPi3.Sensors;
using BrickPi3.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral Deferral { get; set; }
        private readonly Brick Brick = new Brick();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Deferral = taskInstance.GetDeferral();
            Brick.InitSPI();

            var touch = new NXTTouchSensor(Brick, BrickPortSensor.PORT_S1, 100);
            touch.OnStateChanged += new EventHandler<NXTTouchSensorEventArgs>(OnCrash);
        }

        private void OnCrash(object sender, NXTTouchSensorEventArgs e)
        {
            if (e.IsPressed)
                Debug.WriteLine("Crashed into something!");
        }
    }
}
