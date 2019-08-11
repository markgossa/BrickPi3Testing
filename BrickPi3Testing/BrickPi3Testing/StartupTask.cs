using Windows.ApplicationModel.Background;
using System.Diagnostics;
using BrickPi3TestingDependencies;

namespace BrickPi3Testing
{
    public sealed partial class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral Deferral { get; set; }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Deferral = taskInstance.GetDeferral();
            
            var brickConfiguration = new BrickConfiguration();
            var movement = new Movement(brickConfiguration);
            int speed = 15;

            movement.Move(20000, speed, Movement.MoveDirection.Forward, endMovement: true);
            movement.Turn(duration: 20000, turnCircle: 0, speed, endMovement: true);
        }
    }
}
