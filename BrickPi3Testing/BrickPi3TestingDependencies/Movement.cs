using System.Threading;

namespace BrickPi3TestingDependencies
{
    public class Movement
    {
        private BrickConfiguration brickConfiguration;
        public Movement(BrickConfiguration brickConfiguration)
        {
            this.brickConfiguration = brickConfiguration;
        }

        public void Move(int duration, int speed, MoveDirection moveDirection,
            bool endMovement)
        {
            speed = moveDirection.Equals(MoveDirection.Backward)
                ? -speed
                : speed;

            var rightMotorSpeed = speed;
            var leftMotorSpeed = -speed;

            brickConfiguration.RightMotor.SetSpeed(rightMotorSpeed);
            brickConfiguration.LeftMotor.SetSpeed(leftMotorSpeed);
            EndMovement(duration, endMovement);
        }

        private void EndMovement(int duration, bool endMovement)
        {
            if (endMovement && duration != -1)
            {
                Thread.Sleep(duration);
                brickConfiguration.RightMotor.SetSpeed(0);
                brickConfiguration.LeftMotor.SetSpeed(0);
            }
        }

        public void Turn(int duration, int turnCircle, int speed, 
            bool endMovement, TurnDirection direction = TurnDirection.Right)
        {
            var rightMotorSpeed = speed;
            var leftMotorSpeed = -speed;

            // turn right on the spot
            brickConfiguration.RightMotor.SetSpeed(-rightMotorSpeed);
            brickConfiguration.LeftMotor.SetSpeed(leftMotorSpeed);
            EndMovement(duration, endMovement);

            // turn right at same speed
            //if (direction == TurnDirection.Right)
            //{
            //    brickConfiguration.RightMotor.SetSpeed(0);
            //    brickConfiguration.LeftMotor.SetSpeed(0);
            //    brickConfiguration.RightMotor.SetSpeed(speed + (turnCircle * 10));
            //    brickConfiguration.LeftMotor.SetSpeed(-speed);
            //    Task.Delay(duration).Wait();
            //}
        }

        public void TurnAroundOnTheSpot(TurnDirection direction = TurnDirection.Right)
        {
            Turn(duration: 4000, turnCircle: 0, speed: 30, endMovement: true,
                direction: direction);
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
