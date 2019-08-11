using System.Threading.Tasks;

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

            var normalizedSpeeds = NormalizeSpeeds(speed);

            brickConfiguration.RightMotor.SetSpeed(normalizedSpeeds.RightMotorSpeed);
            brickConfiguration.LeftMotor.SetSpeed(normalizedSpeeds.LeftMotorSpeed);
            EndOperation(duration, endMovement);
        }

        private NormalizedSpeed NormalizeSpeeds(int speed)
        {
            return new NormalizedSpeed()
            {
                RightMotorSpeed = speed,
                LeftMotorSpeed = -speed
            };
        }

        private void EndOperation(int duration, bool endMovement)
        {
            if (endMovement && duration != -1)
            {
                Task.Delay(duration).Wait();
                brickConfiguration.RightMotor.SetSpeed(0);
                brickConfiguration.LeftMotor.SetSpeed(0);
            }
        }

        public void Turn(int duration, int turnCircle, int speed, 
            bool endMovement, TurnDirection direction = TurnDirection.Right)
        {
            var normalizedSpeeds = NormalizeSpeeds(speed);

            // turn right on the spot
            brickConfiguration.RightMotor.SetSpeed(-normalizedSpeeds.RightMotorSpeed);
            brickConfiguration.LeftMotor.SetSpeed(normalizedSpeeds.LeftMotorSpeed);
            EndOperation(duration, endMovement);
        }

        public void Stop()
        {
            brickConfiguration.LeftMotor.Stop();
            brickConfiguration.RightMotor.Stop();
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
