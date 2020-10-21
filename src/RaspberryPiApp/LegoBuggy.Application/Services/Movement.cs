using BrickPi3;
using BrickPi3.Models;
using BrickPi3.Movement;
using System.Threading.Tasks;

namespace LegoBuggy.Application.Services
{
    public class Movement : IMovement
    {
        private readonly Motor _leftMotor;
        private readonly Motor _rightMotor;

        public Movement(Brick brick)
        {
            _leftMotor = new Motor(brick, BrickPortMotor.PORT_B);
            _rightMotor = new Motor(brick, BrickPortMotor.PORT_A);
        }

        public void Move(int speed, MoveDirection moveDirection, int duration = -1,
            bool endMovement = false)
        {
            speed = moveDirection.Equals(MoveDirection.Backward)
                ? -speed
                : speed;

            var normalizedSpeeds = NormalizeSpeeds(speed);

            _rightMotor.SetSpeed(normalizedSpeeds.RightMotorSpeed);
            _leftMotor.SetSpeed(normalizedSpeeds.LeftMotorSpeed);
            EndOperation(duration, endMovement);
        }

        public void Turn(int speed, int sharpness, TurnDirection direction,
            int duration = -1, bool endMovement = false)
        {
            sharpness = sharpness < 0 ? 5 : sharpness;
            sharpness = sharpness > 99 ? 99 : sharpness;

            var normalizedSpeeds = NormalizeSpeeds(speed);

            if (direction == TurnDirection.Right)
            {
                int turnModifier = normalizedSpeeds.RightMotorSpeed * 2 * sharpness / 100;

                int rightMotorSpeed = normalizedSpeeds.RightMotorSpeed - turnModifier;
                int leftMotorSpeed = normalizedSpeeds.LeftMotorSpeed;

                _rightMotor.SetSpeed(rightMotorSpeed);
                _leftMotor.SetSpeed(leftMotorSpeed);
            }
            else
            {
                int turnModifier = (100 - sharpness) / 100 * normalizedSpeeds.RightMotorSpeed * 2;
                _rightMotor.SetSpeed(normalizedSpeeds.RightMotorSpeed);
                _leftMotor.SetSpeed(-normalizedSpeeds.LeftMotorSpeed + turnModifier);
            }

            EndOperation(duration, endMovement);
        }

        public void Stop()
        {
            _leftMotor.Stop();
            _rightMotor.Stop();
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
                _rightMotor.SetSpeed(0);
                _leftMotor.SetSpeed(0);
            }
        }
    }
}
