using Iot.Device.BrickPi3;
using Iot.Device.BrickPi3.Models;
using Iot.Device.BrickPi3.Movement;
using Iot.Device.BrickPi3.Sensors;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BrickPi3Testing2
{
    internal class MyHostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken) => await TestNXTTouchSensor();

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping");
        }

        private async Task TestNXTTouchSensor()
        {
            using var brick = new Brick();

            var touch = new NXTTouchSensor(brick, SensorPort.Port2);
            var motor = new Motor(brick, BrickPortMotor.PortB);

            while (true)
            {
                if (touch.IsPressed())
                {
                    Console.WriteLine("Button pressed: True");
                    motor.SetSpeed(10);
                }
                else
                {
                    Console.WriteLine("Button pressed: False");
                    motor.SetSpeed(0);
                }

                await Task.Delay(200);
            }
        }
    }
}