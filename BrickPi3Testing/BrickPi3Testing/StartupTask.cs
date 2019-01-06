using Windows.ApplicationModel.Background;
using BrickPi3;

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
            //TestNXTUltrasonic().Wait();
            TestNXTLightSensor().Wait();

            // Reset
            brick.reset_all();
        }
    }
}
