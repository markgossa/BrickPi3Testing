using BrickPi3;
using BrickPi3.Models;
using BrickPi3.Movement;

namespace LegoBuggy.Application.Services
{
    public class BrickConfiguration
    {
        private readonly Brick brick = new Brick();
        public Motor LeftMotor { get; set; }
        public Motor RightMotor { get; set; }

        public BrickConfiguration()
        {
            brick.InitSPI();
            LeftMotor = new Motor(brick, BrickPortMotor.PORT_A);
            RightMotor = new Motor(brick, BrickPortMotor.PORT_B);
        }
    }
}