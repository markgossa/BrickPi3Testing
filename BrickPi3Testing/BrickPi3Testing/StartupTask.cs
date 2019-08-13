using Windows.ApplicationModel.Background;
using BrickPi3TestingDependencies;
using System.Threading.Tasks;

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
            int speed = 30;

            //movement.Move(speed, Movement.MoveDirection.Forward);
            //Task.Delay(1500 / 5).Wait();
            movement.Turn(speed, 100, Movement.TurnDirection.Right, 2500, true);
            movement.Turn(speed, 75, Movement.TurnDirection.Right, 2500, true);
            movement.Turn(speed, 50, Movement.TurnDirection.Right, 2500, true);
            movement.Turn(speed, 25, Movement.TurnDirection.Right, 2500, true);
            movement.Turn(speed, 0, Movement.TurnDirection.Right, 2500, true);
            movement.Stop();
        }
    }
}
