using BrickPi3;
using BrickPi3.Models;
using BrickPi3.Sensors;
using LegoBuggy.Application.Services;
using System.ComponentModel;
using System.Diagnostics;
using Windows.ApplicationModel.Background;

namespace BrickPi3Testing
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        private readonly Brick _brick = new Brick();
        private NXTTouchSensor _touchSensor;
        private IMovement _movement;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            Initialize();
            RegisterToEvents();
            Move();
        }

        private void Initialize()
        {
            _brick.InitSPI();
            _touchSensor = new NXTTouchSensor(_brick, BrickPortSensor.PORT_S2);
            _movement = new Movement(_brick);
        }

        private void RegisterToEvents() => _touchSensor.PropertyChanged += new PropertyChangedEventHandler(OnTouchSensorChange);


        private void OnTouchSensorChange(object sender, PropertyChangedEventArgs args)
        {
            var isPressed = _touchSensor.IsPressed();
            if (isPressed)
            {
                Debug.WriteLine($"Touch sensor: {isPressed}");
                _movement.Stop();
                Debug.WriteLine("Shutting down...");
                _deferral.Complete();
            }
        }

        private void Move()
        {
            _movement.Move(10, Movement.MoveDirection.Backward, 2);
        }
    }
}
