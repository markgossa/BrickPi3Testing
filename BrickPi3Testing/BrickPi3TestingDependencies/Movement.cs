using System;
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

        public void Move(int speed, MoveDirection moveDirection, int duration = -1, 
            bool endMovement = false)
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

        public void Turn(int speed, int sharpness, TurnDirection direction,
            int duration = -1, bool endMovement = false)
        {
            sharpness = sharpness < 0 ? 5 : sharpness;
            sharpness = sharpness > 99 ? 99 : sharpness;

            var normalizedSpeeds = NormalizeSpeeds(speed);

            if(direction == TurnDirection.Right)
            {
                int turnModifier = normalizedSpeeds.RightMotorSpeed * 2 * sharpness / 100;

                int rightMotorSpeed = normalizedSpeeds.RightMotorSpeed - turnModifier;
                int leftMotorSpeed = normalizedSpeeds.LeftMotorSpeed;

                brickConfiguration.RightMotor.SetSpeed(rightMotorSpeed);
                brickConfiguration.LeftMotor.SetSpeed(leftMotorSpeed);
            }
            else
            {
                int turnModifier = (100 - sharpness) / 100 * normalizedSpeeds.RightMotorSpeed * 2;
                brickConfiguration.RightMotor.SetSpeed(normalizedSpeeds.RightMotorSpeed);
                brickConfiguration.LeftMotor.SetSpeed(-normalizedSpeeds.LeftMotorSpeed + turnModifier);
            }
            
            EndOperation(duration, endMovement);
        }

        public void Stop()
        {
            brickConfiguration.LeftMotor.Stop();
            brickConfiguration.RightMotor.Stop();
        }

        public void TurnAroundOnTheSpot(TurnDirection direction = TurnDirection.Right)
        {
            Turn(duration: 4000, sharpness: 0, speed: 30, endMovement: true,
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
