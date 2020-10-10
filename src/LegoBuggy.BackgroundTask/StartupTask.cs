using BrickPi3;
using BrickPi3.Models;
using BrickPi3.Movement;
using BrickPi3.Sensors;
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
        private Motor _motor1;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            Initialize();
            RegisterToEvents();
        }

        private void Initialize()
        {
            _brick.InitSPI();
            _touchSensor = new NXTTouchSensor(_brick, BrickPortSensor.PORT_S2);
            _motor1 = new Motor(_brick, BrickPortMotor.PORT_B);
        }

        private void RegisterToEvents() => _touchSensor.PropertyChanged += new PropertyChangedEventHandler(OnTouchSensorChange);


        private void OnTouchSensorChange(object sender, PropertyChangedEventArgs args)
        {
            var isPressed = _touchSensor.IsPressed();
            if (isPressed)
            {
                Debug.WriteLine($"Touch sensor: {isPressed}");
                Debug.WriteLine("Shutting down...");
                _deferral.Complete();
            }
        }
    }
}
