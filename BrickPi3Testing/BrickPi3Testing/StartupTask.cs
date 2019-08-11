using Windows.ApplicationModel.Background;
using BrickPi3;
using System.Diagnostics;
using BrickPi3.Movement;
using BrickPi3TestingDependencies;
using static BrickPi3TestingDependencies.Movement;

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

            Debug.WriteLine("Move forwards");
            var stopwatch = new Stopwatch();
            movement.Move(1000, 30, MoveDirection.Forward, endMovement: false);
            Debug.WriteLine($"Stopped after {stopwatch.ElapsedMilliseconds}");

            movement.Turn(duration: 4000, turnCircle: 0, speed: 30, endMovement: true);
        }
    }
}
