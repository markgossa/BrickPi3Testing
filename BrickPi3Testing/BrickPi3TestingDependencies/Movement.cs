using System.Threading.Tasks;

namespace BrickPi3TestingDependencies
{
    public class Movement
    {
        public void Move(int duration, int speed, MoveDirection moveDirection,
            BrickConfiguration brickConfiguration)
        {
            speed = moveDirection.Equals(MoveDirection.Backward)
                ? -speed
                : speed;

            brickConfiguration.LeftMotor.Start();
            brickConfiguration.RightMotor.Start();
            brickConfiguration.LeftMotor.SetSpeed(-speed);
            brickConfiguration.RightMotor.SetSpeed(speed);
            if (duration != -1)
            {
                Task.Delay(duration).Wait();
                brickConfiguration.LeftMotor.SetSpeed(0);
                brickConfiguration.RightMotor.SetSpeed(0);
            }
        }

        public void Turn(int duration, int turnCircle, TurnDirection direction, 
            BrickConfiguration brickConfiguration)
        {
            int speed = 30;
            if (direction == TurnDirection.Right)
            {
                brickConfiguration.RightMotor.SetSpeed(0);
                brickConfiguration.LeftMotor.SetSpeed(0);
                brickConfiguration.RightMotor.SetSpeed(-speed + (turnCircle * 10));
                brickConfiguration.LeftMotor.SetSpeed(speed);
                Task.Delay(duration).Wait();
            }
        }

        public enum TurnDirection
        {
            Right,
            Left
        }

        public enum MoveDirection
        {
            Forward,
            Backward
        }
    }
}
